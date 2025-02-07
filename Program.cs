var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();



app.MapGet("/instruction", () =>
{
    return new {
        Title = "How to wash your hair",
        Explanation = "To wash your hair, wet it thoroughly with warm water. " +
        "Apply a small amount of shampoo to your scalp, lather, and gently massage with your fingertips." +
        "Rinse out the shampoo completely. Then, apply conditioner to the ends of your hair, avoiding the scalp." +
        "Leave it for a few minutes before rinsing with cool water. Gently towel-dry your hair or let it air dry."
    };
});

app.MapGet("/BaseShampoo", () =>
{
    return new {
        Title = "BASE Bodyworks Shampoo",
        Ingredients = "Water (H₂O), Coco-Glucoside, Glycerin, Guar Gum, Aloe Vera, " +
        "Olivem1000 (Cetearyl Olivate and Sorbitan Olivate), Hydrolyzed Keratin, Argan Oil," +
        " Coconut Oil, Avocado Oil, Vitamin E, Peppermint Oil, Potassium Sorbate, Sodium Benzoate"
    };
});



app.Run();
