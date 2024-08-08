using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.ProductAPI.Data;
using Services.ProductAPI.Models;
using Services.ProductAPI.Models.Dto;

namespace Services.product.Controllers
{
    [ApiController]
    [Route("/api/product")]
    [Authorize]
    public class ProductApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;
        public ProductApiController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper =  mapper;
            _responseDto = new ResponseDto();
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> products = _db.Products.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
                
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message.ToString();
            }
            return _responseDto;
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product product = _db.Products.First(u=> u.ProductId == id);
                _responseDto.Result = _mapper.Map<ProductDto>(product); 
                
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message=ex.Message.ToString();
            }
            return _responseDto;
        }
        //[HttpGet]
        //[Route("GetByCode/{code}")]
        //public ResponseDto GetByCode(string code)
        //{
        //    try
        //    {
        //        Models.product product = _db.products.First(u => u.productCode.ToLower() == code.ToLower());
        //        _responseDto.Result = _mapper.Map<productDto>(product); 

        //    }
        //    catch (Exception ex)
        //    {
        //        _responseDto.IsSuccess = false;
        //        _responseDto.Message = ex.Message.ToString();
        //    }
        //    return _responseDto;
        //}
        [HttpPost]
        public ResponseDto Post([FromBody] ProductDto productDto)
        {
            try
            {

                Product product = _mapper.Map<Product>(productDto);
                _db.Products.Add(product);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<ProductDto>(product); 
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message.ToString();
            }
            return _responseDto;
        }
        [HttpPut]
        public ResponseDto Put([FromBody] ProductDto productDto)
        {
            try
            {

                Product product = _mapper.Map<Product>(productDto);
                _db.Products.Update(product);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message.ToString();
            }
            return _responseDto;
        }
        [HttpDelete]
        [Route("{productId:int}")]
        [Authorize(Roles ="ADMIN")]
        public ResponseDto Delete(int productId)
        {
            try
            {
                Product product = _db.Products.First(u => u.ProductId == productId);
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message.ToString();
            }
            return _responseDto;
        }
    }
}
