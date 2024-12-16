using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Discout.Grpc;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
	public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger):DiscountProtoService.DiscountProtoServiceBase
	{
		public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
		{
			var coupon = await dbContext.Coupons.FirstOrDefaultAsync(d => d.ProductName == request.ProductName);

			
			if(coupon is null) 
				coupon = new Models.Coupon() { Id=0, Amount=0, Description="Product with no Discount", ProductName="No Discount"};
		
			var response = coupon.Adapt<CouponModel>();
			logger.LogInformation("Discount retrived for the product: {productName}, Amount: {amount}, Id: {id} and Requested Product: {request}", coupon.ProductName, coupon.Amount, coupon.Id, request.ProductName);
			return response;
		}
		public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
		{
			var coupon = request.Coupon.Adapt<Coupon>();
			if(coupon is null)
			{
				logger.LogError("{DateTime} [Error] error upon creating the following object {request}",DateTime.UtcNow, request);
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
			}
			 dbContext.Coupons.Add(coupon);
			await dbContext.SaveChangesAsync();
			logger.LogInformation("Discount successfully created: {productName}, Amount: {amount}, Id: {id}", coupon.ProductName, coupon.Amount, coupon.Id);

			var response = coupon.Adapt<CouponModel>();
			return response;
		}
		public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
		{
			var coupon = request.Coupon.Adapt<Coupon>();
			if (coupon is null)
			{
				logger.LogError("{DateTime} [Error] error upon creating the following object {request}", DateTime.UtcNow, request);
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
			}
			dbContext.Coupons.Update(coupon);
			await dbContext.SaveChangesAsync();
			logger.LogInformation("Discount successfully updated: {productName}, Amount: {amount}, Id: {id}", coupon.ProductName, coupon.Amount, coupon.Id);

			var response = coupon.Adapt<CouponModel>();
			return response;
		}
		public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
		{
			var coupon = await dbContext.Coupons.FirstOrDefaultAsync(d => d.ProductName == request.ProductName);


			if (coupon is null)
				throw new RpcException(new Status(StatusCode.NotFound, $"Discount with productName {request.ProductName} does not exist"));

			dbContext.Coupons.Remove(coupon);
			await dbContext.SaveChangesAsync();
			logger.LogInformation("Discount deleted successfully for the product: {productName}", coupon.ProductName);

			return new DeleteDiscountResponse() { IsSuccessful=true};
		}
	}
}
