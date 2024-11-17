
namespace Catalog_API.Products.DeleteProduct
{
	public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

	public record DeleteProductResult(bool IsDeleted);
	public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
	{
		public DeleteProductCommandValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is Required");
		}
		internal class DeleteProductHandler(IDocumentSession _session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
		{
			public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
			{

				var product = await _session.LoadAsync<Product>(command.Id, cancellationToken);

				if (product is null)
				{
					throw new ProductNotFoundException(command.Id);
				}
				_session.Delete<Product>(product.Id);
				await _session.SaveChangesAsync(cancellationToken);
				return new DeleteProductResult(true);

			}
		}
	}
}
