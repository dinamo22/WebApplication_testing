using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;
using System.Text;

#region
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

#region Image SendFileAsync testing downloading
//app.Run(async (context) => await context.Response.WriteAsync($"Path: {context.Request.Path} "));
//app.Run(async (context) => await context.Response.SendFileAsync("D:\\test.jpg"));
//app.Run(async (context) => await context.Response.SendFileAsync("test.jpg"));


//���� ���������� ����� ����� ������ ��� ������
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

//    // ���� ��������� ���� �� ������ "/postuser", �������� ������ �����
//    if (context.Request.Path == "/postuser")
//    {
//        //�����, ���� �������� ����� "/postuser", �� ��������������, ��� ���������� ��������� �����. ������� �������� ������������ ����� � ���������� form
//        //�������� Request.Form ���������� ������ IFormCollection - ������ ���� �������, ��� �� ����� ����� �������� �������� ��������. ��� ���� � �������� ������ ��������� �������� ����� ���� (�������� ��������� name ��������� �����):
//        var form = context.Request.Form;
//        string name = form["name"];
//        string age = form["age"];
//        string dick = form["dick"];
//        //����� ��������� ������ ����� ��� ������������ ������� �������:
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

//    // ���� ��������� ���� �� ������ "/postuser", �������� ������ �����
//    if (context.Request.Path == "/postuser")
//    {
//        var form = context.Request.Form;
//        string name = form["name"];
//        string age = form["age"];
//        string[] languages = form["languages"];
//        // ������� �� ������� languages ���� ������
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

#region ������������
//app.Run(async (context) =>
//{
//    if (context.Request.Path == "/old")
//    {
//        context.Response.Redirect("/new");
//    }
//    //�������������
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
    public string Name { get; set; }
    public string Address { get; set; }
    Armor Person_armor { get; set; }
    public Person(string name, string address)
    {
        Name = name;
        Address = address;
    }
    public Person(Armor person_armor) : this (Name, Adress)
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

    public Armor(string? name)
    {
        this.name = name;
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
#endregion