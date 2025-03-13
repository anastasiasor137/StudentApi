using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер
builder.Services.AddControllers();

var app = builder.Build();

// Настройка конвейера обработки запросов
if (app.Environment.IsDevelopment())
{
    // Использовать разработческий хост или что-то подобное
    app.UseDeveloperExceptionPage();
}
else
{
    // Для продакшена можно использовать обработку ошибок
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Использовать маршрутизацию
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

// Маршрутизация запросов к контроллерам
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Запуск приложения
app.Run();