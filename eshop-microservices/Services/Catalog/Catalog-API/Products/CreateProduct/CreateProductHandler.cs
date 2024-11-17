


namespace Catalog_API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator() { 
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
			RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
		}
	}

    internal class CreateProductCommandHandler (IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult> { //injected the marten session in the database
		 
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

			try
			{
				//Create product entity from command object
				//save to database
				//return CreateProductResult

				var product = new Product
				{
					Name = command.Name,
					Category = command.Category,
					Description = command.Description,
					ImageFile = command.ImageFile,
					Price = command.Price,
				};
				//save to database
				//...
				session.Store(product); //store the database in the server 
				await session.SaveChangesAsync(cancellationToken); //cancels the cancelation token


				//return Result

				return new CreateProductResult(product.Id);
			}
			catch (Exception ex) {
				throw new Exception(ex.Message);
			}
            
        }
    }
}
