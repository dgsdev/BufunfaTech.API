using BufunfaTech.API.Context;
using BufunfaTech.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<Context>
   (option =>
   option.UseNpgsql("Host=ep-withered-shape-94172124.us-east-1.aws.neon.tech;Port=5432;Pooling=true;Database=bufunfatechdb;User Id=dgsdev;Password=uWwh3IRijV6D;"));

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// M�todo criar transa��o
app.MapPost("transactions", async (Context context, Transaction transaction) =>
{
    if (transaction.Date == DateTime.MinValue)
    {
        transaction.Date = DateTime.UtcNow; 
    }
    await context.Transaction.AddAsync(transaction);
    await context.SaveChangesAsync();   
})
.WithName("PostTransaction")
.WithOpenApi();

// M�todo atualizar transa��o
app.MapPut("transactions/{id}", async (Context context, int id, Transaction transaction) =>
{    
    var transactionToUpdate = await context.Transaction.FindAsync(id);
    if (transactionToUpdate != null)
    {
        transactionToUpdate.Type = transaction.Type;
        transactionToUpdate.Category = transaction.Category;
        transactionToUpdate.Title = transaction.Title;
        transactionToUpdate.Amount = transaction.Amount;
        transactionToUpdate.Date = transaction.Date;
        transactionToUpdate.Day = transaction.Day;
        transactionToUpdate.Month = transaction.Month;
        transactionToUpdate.Year = transaction.Year;
        transactionToUpdate.Note = transaction.Note;

        await context.SaveChangesAsync();
        return Results.Ok(transactionToUpdate);
    }
    else
    {
        return Results.NotFound();
    }
    
})
.WithName("PutTransaction")
.WithOpenApi();

// M�todo atualizar transa��o parcialmente
app.MapPatch("transactions/{id}", async (Context context, int id, Transaction transaction) =>
{
    var transactionToUpdate = await context.Transaction.FindAsync(id);
    if (transactionToUpdate != null)
    {
        if (transaction.Type != null)
        {
            transactionToUpdate.Type = transaction.Type;
        }
        if (transaction.Category != null)
        {
            transactionToUpdate.Category = transaction.Category;
        }
        if (transaction.Title != null)
        {
            transactionToUpdate.Title = transaction.Title;
        }
        if (transaction.Amount != 0)
        {
            transactionToUpdate.Amount = transaction.Amount;
        }
        if (transaction.Date != DateTime.MinValue)
        {
            transactionToUpdate.Date = transaction.Date;
        }
        if (transaction.Day != 0)
        {
            transactionToUpdate.Day = transaction.Day;
        }
        if (transaction.Month != 0)
        {
            transactionToUpdate.Month = transaction.Month;
        }
        if (transaction.Year != 0)
        {
            transactionToUpdate.Year = transaction.Year;
        }
        if (transaction.Note != null)
        {
            transactionToUpdate.Note = transaction.Note;
        }

        await context.SaveChangesAsync();
        return Results.Ok(transactionToUpdate);
    }
    else
    {
        return Results.NotFound();
    }
   
})
.WithName("PatchTransaction")
.WithOpenApi();

// M�todo listar transa��es
app.MapGet("transactions", async (Context context) =>
{
    return await context.Transaction.ToListAsync();

})
.WithName("GetTransactions")
.WithOpenApi();

// M�todo listar transa��es por data
app.MapGet("transactions/{id}", async (Context context, int id) =>
{
    return await context.Transaction.FindAsync(id);

})
.WithName("GetTransactionById")
.WithOpenApi();

// M�todo deletar transa��o
app.MapDelete("transactions/{id}", async (Context context, int id) =>
{
    var transactionToDelete = await context.Transaction.FindAsync(id);
    if (transactionToDelete != null)
    {
        context.Transaction.Remove(transactionToDelete);
        await context.SaveChangesAsync();
    }        
})
.WithName("DeleteTransaction")
.WithOpenApi();

app.Run();




