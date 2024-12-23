using System.Security.Claims;
using System.Xml;
using AutoMapper;
using FluentValidation;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Pharmacy.DAL.Interfaces;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.ModelsDb;
using Pharmacy.Domain.Response;
using Pharmacy.Service.Interfaces;
using Pharmacy.Domain.Enum;
using Pharmacy.DAL.Storage;
using Pharmacy.Domain.Helpers;
using Pharmacy.Domain.Validators;

namespace Pharmacy.Service.Service;

public class AccountService : IAccountServise
{
    private readonly IBaseStorage<UserDb> _userStorage;
    private UserValidator _validationsrules { get; set; }
    private IMapper _mapper { get; set; }

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public AccountService(IBaseStorage<UserDb> useStorage)
    {
        _userStorage = useStorage;
        _mapper = mapperConfiguration.CreateMapper();
        _validationsrules = new UserValidator();
    }

    public async Task<BaseResponse<ClaimsIdentity>> Login(User model)
    {
        try
        {
            await _validationsrules.ValidateAndThrowAsync(model);
            var userdb = await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);
            if (userdb == null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Пользователь не найден"
                };
            }

            if (userdb.Password != HashPasswordHelper.HashPassword(model.Password))
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Неверный пароль или почта"
                };
            }

            model = _mapper.Map<User>(userdb);
            var result = AuthenticateUserHelper.Authenticate(model);
            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (ValidationException exception)
        {
            var errorMessage=string.Join(";",exception.Errors.Select(e=>e.ErrorMessage));
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = errorMessage,
                StatusCode = StatusCode.BadRequest
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<string>> Register(User model)
    {
        try
        {
            Random random = new Random();
            string confirmationCode = $"{random.Next(10)}{random.Next(10)}{random.Next(10)}{random.Next(10)}";
            //model.PathImage = "/Images/designer.jpg";
            //model.CreatedAt=DateTime.Now;
            //model.Password = HashPasswordHelper.HashPassword(model.Password);

            //await _validationsrules.ValidateAndThrowAsync(model);

            //var userdb = _mapper.Map<UserDb>(model);
            if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) != null)
            {
                return new BaseResponse<string>()
                {
                    Description = "Пользователь с такой почтой уже есть"
                };
            }

            await SendEmail(model.Email,confirmationCode);
            //await _userStorage.Add(userdb);
            //var result = AuthenticateUserHelper.Authenticate(model);
            return new BaseResponse<string>()
            {
                Data = confirmationCode,
                Description = "Письмо отправлено",
                StatusCode = StatusCode.OK
            };
        }
        catch (ValidationException exception)
        {
            var errorMessage=string.Join(";",exception.Errors.Select(e=>e.ErrorMessage));
            return new BaseResponse<string>()
            {
                Description = errorMessage,
                StatusCode = StatusCode.BadRequest
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<string>()
            {
                Description = e.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

 public async Task SendEmail(string email, string confirmationCode)
{
    string path = @"D:\Практика(Зимняя)\Проекты\Pharmacy\password.txt";
    var emailMessage = new MimeMessage();

    emailMessage.From.Add(new MailboxAddress("Администрация PillParadice", "Vdox.ru"));
    emailMessage.To.Add(new MailboxAddress("", email));
    emailMessage.Subject = "Подтверждение регистрации";

    // Тело письма с использованием HTML-шаблона
    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
    {
        Text = @"
            <!DOCTYPE html>
            <html lang='ru'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Подтверждение Email</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        background-color: #f9f9f9;
                        margin: 0;
                        padding: 0;
                    }
                    .container {
                        max-width: 600px;
                        margin: 20px auto;
                        padding: 20px;
                        background-color: #fff;
                        border-radius: 10px;
                        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
                    }
                    .header {
                        text-align: center;
                        margin-bottom: 20px;
                    }
                    .header h1 {
                        color: #FF77B1;
                        font-size: 24px;
                        margin: 0;
                    }
                    .message {
                        font-size: 16px;
                        line-height: 1.6;
                        color: #333;
                    }
                    .code-container {
                        text-align: center;
                        margin: 20px 0;
                    }
                    .code {
                        display: inline-block;
                        background-color: #f2f2f2;
                        padding: 10px 20px;
                        border-radius: 8px;
                        font-size: 20px;
                        font-weight: bold;
                        letter-spacing: 2px;
                        color: #FF77B1;
                    }
                    .footer {
                        text-align: center;
                        margin-top: 20px;
                        font-size: 14px;
                        color: #777;
                    }
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h1>Добро пожаловать в PillParadice!</h1>
                    </div>
                    <div class='message'>
                        <p>Спасибо за регистрацию на нашем сайте! Мы рады, что вы выбрали PillParadice.</p>
                        <p>Для завершения регистрации, пожалуйста, используйте код подтверждения ниже:</p>
                    </div>
                    <div class='code-container'>
                        <div class='code'>" + confirmationCode + @"</div>
                    </div>
                    <div class='message'>
                        <p>Если вы не регистрировались на нашем сайте, просто проигнорируйте это письмо.</p>
                    </div>
                    <div class='footer'>
                        <p>С наилучшими пожеланиями, команда PillParadice</p>
                        <p>&copy; 2024 PillParadice. Все права защищены.</p>
                    </div>
                </div>
            </body>
            </html>"
    };

    using (StreamReader reader = new StreamReader(path))
    {
        string password = await reader.ReadToEndAsync(); // Прочтение пароля из файла
        using (var client = new SmtpClient())
        {
            // Подключение к SMTP-серверу
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync("kirillmarkor@gmail.com", password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}


    public async Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(User model, string code, string confirmationCode)
    {
        try
        {
            if (code != confirmationCode)
            {
                throw new Exception("Неверный код! Регистрация не выполнена.");
            }
            model.PartImage = "/Images/designer.jpg";
            model.CreatedAt=DateTime.Now;
            model.Password=HashPasswordHelper.HashPassword(model.Password);
            
            await _validationsrules.ValidateAndThrowAsync(model);
            
            var userdb=_mapper.Map<UserDb>(model);
            await _userStorage.Add(userdb);
            
            var result = AuthenticateUserHelper.Authenticate(model);
            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "Объект добавился",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = e.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<BaseResponse<ClaimsIdentity>> IsCreatedAccount(User model)
    {
        try
        {
            var userDb = new UserDb();

            // Проверяем, существует ли пользователь с таким email
            if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) == null)
            {
                model.Password = "google";
                model.CreatedAt = DateTime.Now;

                userDb = _mapper.Map<UserDb>(model);
                await _userStorage.Add(userDb);

                // Аутентификация нового пользователя
                var resultRegister = AuthenticateUserHelper.Authenticate(model);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = resultRegister,
                    Description = "Объект добавился",
                    StatusCode = StatusCode.OK
                };
            }
            else
            {
                // Если пользователь уже существует, аутентифицируем его
                var resultLogin = AuthenticateUserHelper.Authenticate(model);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = resultLogin,
                    Description = "Объект уже был создан",
                    StatusCode = StatusCode.OK
                };
            }
        }
        catch (Exception ex)
        {
            // Обработка ошибок
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public User GetUserByEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentException("Email пустой или нулевой", nameof(email));

        var user = _userStorage.GetAll().FirstOrDefault(u => u.Email == email);
        User users = _mapper.Map<User>(user);
        return users;
    }
}