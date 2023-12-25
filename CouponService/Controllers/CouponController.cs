﻿using AutoMapper;
using CouponService.Data.Dto;
using CouponService.Models;
using CouponService.Services.Iservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CouponService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly Icoupon _couponservice;
        private readonly IMapper _mapper;
        private readonly ResponseDTO _responsedto;
        public CouponController(Icoupon couponservice , IMapper mapper)
        {
            _couponservice = couponservice;
            _mapper = mapper;
            _responsedto = new ResponseDTO();
            
        }


        [HttpGet]

        public async Task<ActionResult<ResponseDTO>> GetAllCoupons()
        {

            var coupons = await _couponservice.GetAllCoupons();

            _responsedto.Result = coupons;
            return Ok(_responsedto);

        }


        [HttpGet("{Id}")]


         public async Task<ActionResult<ResponseDTO>> GetCoupon(Guid Id)
        {

            var coupon = await _couponservice.GetCoupon(Id);

            if(coupon == null)
            {
                _responsedto.ErrorMessage = "Not Found";
                _responsedto.Issuccess = false;
                return NotFound(_responsedto);
            }

            _responsedto.Result = coupon;
            return Ok(_responsedto);

        }


        [HttpGet("Get/{code}")]


        public async Task<ActionResult<ResponseDTO>> GetCoupon(string code)
        {

            var coupon = await _couponservice.GetCouponByCode(code);

            if (coupon == null)
            {
                _responsedto.ErrorMessage = "Not Found";
                _responsedto.Issuccess = false;
                return NotFound(_responsedto);
            }

            _responsedto.Result = coupon;
            return Ok(_responsedto);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ResponseDTO>> GetCoupon(AddCouponDto newcoupon)
        {

            var mappedcoupon = _mapper.Map<Coupon>(newcoupon);
            var response = await _couponservice.AddCoupon(mappedcoupon);

            _responsedto.Result = response;
            return Ok(_responsedto);

        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]


        public async Task<ActionResult<ResponseDTO>> DeleteCoupon(Guid Id)
        {

            var coupon = await _couponservice.GetCoupon(Id);
            var response = await _couponservice.DeleteCoupon(coupon);

            _responsedto.Result = response;
            return Ok(_responsedto);
        }

        [HttpPut("{Id}")]

        [Authorize(Roles ="Admin")]

        public async Task<ActionResult<ResponseDTO>> UpdateCoupon(Guid Id , UpdateCouponDTO updatedcoupon) {
        
            var existingcoupon = await _couponservice.GetCoupon(Id);

            if(existingcoupon != null)
            {
                // use automapper to update the provided fields

                var updated = _mapper.Map(updatedcoupon,existingcoupon);

                 await _couponservice.UpdateCoupon();

                _responsedto.Result = updated;

                return Ok(_responsedto);

            }

            return BadRequest("The coupon not found");
        
        
        }
    }



}
