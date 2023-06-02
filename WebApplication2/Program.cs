using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;
using System.Runtime.Loader;
using System.Text;

#region localhost/api/hosts
HttpClient client = new HttpClient();
var pass = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
System.Text.ASCIIEncoding.ASCII.GetBytes($"{"admin"}:{"admin"}")));
client.DefaultRequestHeaders.Authorization = pass;
var stringContent = new StringContent("", Encoding.UTF8, "application/json");
var smth_content = new StringContent("");
HttpResponseMessage response = await client.GetAsync("http://localhost/api/hosts");
var responseContent = await response.Content.ReadAsStringAsync();
#endregion

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

#region Query get request
//Свойство QueryString позволяет получить строку запроса. Строка запроса представляет ту часть запрошенного адреса, которая идет после символа ? и представляет набор параметров, разделенных символом амперсанда &:
//app.Run(async (context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";
//    await context.Response.WriteAsync($"<p>Path: {context.Request.Path}</p>" +
//        $"<p>QueryString: {context.Request.QueryString}</p>");
//});
//С помощью свойства Query можно получить все параметры строки запроса в виде словаря:
//app.Run(async (context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";
//    var stringBuilder = new System.Text.StringBuilder("<h3>Параметры строки запроса</h3><table>");
//    stringBuilder.Append("<tr><td>Параметр</td><td>Значение</td></tr>");
//    foreach (var param in context.Request.Query)
//    {
//        stringBuilder.Append($"<tr><td>{param.Key}</td><td>{param.Value}</td></tr>");
//    }
//    stringBuilder.Append("</table>");
//    await context.Response.WriteAsync(stringBuilder.ToString());
//});
////Соответственно можно вытащить из словаря Query значения отдельных параметров:
//app.Run(async (context) =>
//{
//    string name = context.Request.Query["name"];
//    string age = context.Request.Query["age"];
//    await context.Response.WriteAsync($"{name} - {age}");
//});
#endregion

#region Image SendFileAsync testing downloading
//app.Run(async (context) => await context.Response.WriteAsync($"Path: {context.Request.Path} "));
//app.Run(async (context) => await context.Response.SendFileAsync("D:\\test.jpg"));
//app.Run(async (context) => await context.Response.SendFileAsync("test.jpg"));


//Идет скачивание файла сразу вместо его показа
//app.Run(async (context) =>
//{
//    context.Response.Headers.ContentDisposition = "attachment; filename=download_file.jpg";
//    await context.Response.SendFileAsync("test.jpg");
//});
//app.Run(async (context) =>
//{
//    var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
//    var fileinfo = fileProvider.GetFileInfo("test.jpg");

//    context.Response.Headers.ContentDisposition = "attachment; filename=download_file_2.jpg";
//    await context.Response.SendFileAsync(fileinfo);
//});
#endregion

#region IO with forms
//app.Run(async (context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";

//    // если обращение идет по адресу "/postuser", получаем данные формы
//    if (context.Request.Path == "/postuser")
//    {
//        //Здесь, если запрошен адрес "/postuser", то предполагается, что отправлена некоторая форма. Сначала получаем отправленную форму в переменную form
//        //Свойство Request.Form возвращает объект IFormCollection - своего рода словарь, где по ключу можно получить значение элемента. При этом в качестве ключей выступает названия полей форм (значения атрибутов name элементов формы):
//        var form = context.Request.Form;
//        string name = form["name"];
//        string age = form["age"];
//        string dick = form["dick"];
//        //После получения данных формы они отправляются обратно клиенту:
//        await context.Response.WriteAsync($"<div><p>Name: {name}</p><p>Age: {age}</p><p>BigDick: {dick}</p></div>");
//    }
//    else
//    {
//        await context.Response.SendFileAsync("html/testIO_Dick.html");
//    }
//});

//app.Run(async (context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";

//    // если обращение идет по адресу "/postuser", получаем данные формы
//    if (context.Request.Path == "/postuser")
//    {
//        var form = context.Request.Form;
//        string name = form["name"];
//        string age = form["age"];
//        string[] languages = form["languages"];
//        // создаем из массива languages одну строку
//        string langList = "";
//        foreach (var lang in languages)
//        {
//            langList += $" {lang}";
//        }
//        await context.Response.WriteAsync($"<div><p>Name: {name}</p>" +
//            $"<p>Age: {age}</p>" +
//            $"<div>Languages:{langList}</div></div>");
//    }
//    else if(context.Request.Path == "/phoenix")
//    {
//        await context.Response.SendFileAsync("html/Phoenix.html");
//    }
//    else
//    {
//        await context.Response.SendFileAsync("html/testIO_Massive.html");
//    }
//});

#endregion

#region переаресация
//app.Run(async (context) =>
//{
//    if (context.Request.Path == "/old")
//    {
//        context.Response.Redirect("/new");
//    }
//    //переадресация
//    else if (context.Request.Path == "/new")
//    {
//        await context.Response.WriteAsync("New Page");
//    }
//    else
//    {
//        await context.Response.WriteAsync("Main Page");
//    }
//});
#endregion

#region IO with json
//app.Run(async (context) =>
//{

//    var Armor = new Armor(80);
//    var Orc = new Person("Adolf", "Hilter", Armor);
//    var response = context.Response;
//    await response.WriteAsJsonAsync<Person>(Orc);
//});

//Пример где я ввожу данные в в форме на странице ArmoredPersones.html и создаю класс, который потом отправляю в виде json
//app.Run(async (context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";
//    if (context.Request.Path == "/add_orc")
//    {
//        var form = context.Request.Form;
//        var OrcName = form["Orc name"];
//        var OrcHome = form["Orc home"];
//        var ArmorName = form["Armor name"];
//        var ArmorLvl = form["Armor LVL"];
//        var Armor = new Armor(ArmorName, Convert.ToInt32(ArmorLvl));
//        var Orc = new Person(OrcName, OrcHome, Armor);
//        await context.Response.WriteAsJsonAsync<Person>(Orc);
//    }
//    else
//    {
//        await context.Response.SendFileAsync("html/ArmoredPersones.html");
//    }
//});

//Пример где я ввожу строку запроса, парсю и создаю экземпляр класса Person, после чего отправляют обратно
//app.Run(async(context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";
//    if (context.Request.Path == "/add_orc_v2")
//    {
//        string? OrcName = "";
//        string? OrcHome = "";
//        string? ArmorName = "";
//        int ArmorLvl = 0;
//        if (context.Request.Query.Count > 0)
//        {
//            foreach (var query in context.Request.Query)
//            {
//                if (query.Key == "name")
//                {
//                    if (query.Value.ToString() is null or "")
//                    {
//                        var stringBuilder = new System.Text.StringBuilder("Значение name должно быть не пустым");
//                        stringBuilder.Append("<tr></tr>");
//                        await context.Response.WriteAsync(stringBuilder.ToString());
//                        return;
//                    }
//                    else
//                    {
//                        OrcName = query.Value;
//                    }
//                }
//                else if (query.Key == "home")
//                {
//                        OrcHome = query.Value;
//                }
//                else if (query.Key == "armorName")
//                {
//                    ArmorName = query.Value;
//                }
//                else if (query.Key == "armorLVL")
//                {
//                    ArmorLvl = Convert.ToInt32(query.Value);
//                }
//                else
//                {
//                    await context.Response.WriteAsync("Принимаются лишь параметры name home armorName armorLVL <br> ");
//                }
//            }
//            var Armor = new Armor(ArmorName, Convert.ToInt32(ArmorLvl));
//            var Orc = new Person(OrcName, OrcHome, Armor);
//            await context.Response.WriteAsJsonAsync<Person>(Orc);
//        }
//        else
//        {

//            await context.Response.WriteAsync($"Вы должны ввести хотя бы имя орка и лвл брони<br>");
//        }        
//    }
//    else
//    {
//        await context.Response.SendFileAsync("html/ArmoredPersonesV2.html");
//    }
//});

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    if (request.Path == "/api/user")
    {
        var message = "Некорректные данные";   // содержание сообщения по умолчанию
        try
        {
            // пытаемся получить данные json
            var person = await request.ReadFromJsonAsync<Golum>();
            if (person != null) // если данные сконвертированы в Person
                message = $"Name: {person.Name}  Age: {person.Age}";
        }
        catch { }
        // отправляем пользователю данные
        await response.WriteAsJsonAsync(new { text = message });
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/ReadJsonPerson.html");
    }
});
#endregion



app.Run(async (context) =>
{
    var path = context.Request.Path;
    var fullPath = $"html/{path}";
    var response = context.Response;

    response.ContentType = "text/html; charset=utf-8";
    if (File.Exists(fullPath))
    {
        await response.SendFileAsync(fullPath);
    }
    else
    {
        response.StatusCode = 404;
        await response.WriteAsync("<h2>Not Found</h2>");
    }
});

app.Run();

#region SomeClasses
public class Person
{
    public string? Name { get; private set; }
    public string? Address { get; private set; }
    public Armor? Person_armor { get; set; }
    public Person(string name, string address)
    {
        if (name == null || name == "")
        {
            name = "Adolf";
        }
        else
        {
            Name = name;
        }
        if (address == null || address == "")
        {
            address = "Durotar";
        }
        else
        {
            Address = address;
        }
    }
    public Person(Armor person_armor) : this ("Alolf","Hitler")
    {
        Person_armor = person_armor;
    }

    public Person(string name, string address, Armor? person_armor) : this(name, address)
    {
        Person_armor = person_armor;
    }
}
public class Armor
{
    private string? name;
    private int item_LVL;
    public string Name
    {
        get { return name; }
    }
    public int Item_LVL
    {
        get { return item_LVL; }
    }
    public Armor(string? name)
    {
        if (name is null or "")
        {
            name = "Armor";
        }
        else
        {
            this.name = name;
        }
    }
    public Armor(int item_LVL) : this("Adolf")
    {
        this.item_LVL = item_LVL;
    }
    public Armor(string? name, int item_LVL) : this(name)
    {
        this.item_LVL = item_LVL;
    }

}

public record Golum(string Name, int Age);
#endregion