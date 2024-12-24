using Assignment.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Assignment.Controllers
{
    public class DonHangController : Controller
    {
        private readonly AsmC4Context _context;

        public DonHangController(AsmC4Context context)
        {
            _context = context;
        }

        private int GetCustomerId()
        {
            var customerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (customerIdClaim != null && int.TryParse(customerIdClaim.Value, out int customerId))
            {
                return customerId;
            }

            throw new UnauthorizedAccessException("Không tìm thấy ID khách hàng.");
        }

        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            if (!User.Identity.IsAuthenticated)
            {
                var returnUrl = Url.Action("AddToCart", new { productId, quantity });
                return Challenge(new AuthenticationProperties { RedirectUri = returnUrl });
            }

            var customerId = GetCustomerId();

            // Tìm giỏ hàng của khách hàng hiện tại
            var cart = await _context.DonHangs
                .FirstOrDefaultAsync(c => c.IdUser == customerId);

            if (cart == null)
            {
                cart = new DonHang
                {
                    IdUser = customerId,
                    NgayDat = DateOnly.FromDateTime(DateTime.Now),
                    TrangThai = "Pending" // Đặt giá trị mặc định cho TrangThai
                };
                _context.DonHangs.Add(cart);
                await _context.SaveChangesAsync();
            }

            // Tìm chi tiết đơn hàng
            var cartDetail = await _context.ChiTietDonHangs
                .FirstOrDefaultAsync(cd => cd.IdDonHang == cart.IdDonHang && cd.IdSanPham == productId);

            if (cartDetail != null)
            {
                cartDetail.SoLuong += quantity;
            }
            else
            {
                var product = await _context.SanPhams.FindAsync(productId);
                if (product == null)
                {
                    return NotFound();
                }

                cartDetail = new ChiTietDonHang
                {
                    IdDonHang = cart.IdDonHang,
                    IdSanPham = productId,
                    SoLuong = quantity,
                    GiaBan = product.Gia
                };
                _context.ChiTietDonHangs.Add(cartDetail);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "CartDetail");
        }

    }
}
