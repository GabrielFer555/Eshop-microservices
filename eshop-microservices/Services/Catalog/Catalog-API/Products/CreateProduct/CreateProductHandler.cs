
namespace Catalog_API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler (IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult> { //injected the marten session in the database

        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Create product entity from command object
            //save to database
            //return CreateProductResult

            var product = new Product
            {
                Name=command.Name,
                Category=command.Category,
                Description=command.Description,
                ImageFile=command.ImageFile,
                Price=command.Price,
            };
            //save to database
            //...
            session.Store(product); //store the database in the server 
            await session.SaveChangesAsync(cancellationToken); //cancels the cancelation token


            //return Result

            return new CreateProductResult(product.Id);
        }
    }
}
