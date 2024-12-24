using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment.Models;
using System.Security.Claims;

namespace Assignment.Controllers
{
    public class ChiTietDonHangsController : Controller
    {
        private readonly AsmC4Context _context;

        public ChiTietDonHangsController(AsmC4Context context)
        {
            _context = context;
        }

        // GET: ChiTietDonHangs
        public async Task<IActionResult> Index()
        {
            int customerId = GetCurrentCustomerId();

            // Tìm giỏ hàng của khách hàng hiện tại
            var cart = await _context.DonHangs
                .FirstOrDefaultAsync(c => c.IdUser == customerId);

            if (cart == null)
            {
                // Nếu không có giỏ hàng, trả về view trống hoặc thông báo không có dữ liệu
                return View(new List<ChiTietDonHang>());
            }

            // Lấy các chi tiết đơn hàng cho giỏ hàng của người dùng hiện tại
            var chiTietDonHangs = await _context.ChiTietDonHangs
                .Where(cd => cd.IdDonHang == cart.IdDonHang)
                .Include(cd => cd.IdSanPhamNavigation)
                .ToListAsync();

            return View(chiTietDonHangs); ;
        }

        // GET: ChiTietDonHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHangs
                .Include(c => c.IdDonHangNavigation)
                .Include(c => c.IdSanPhamNavigation)
                .FirstOrDefaultAsync(m => m.IdChiTietDonHang == id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs/Create
        public IActionResult Create()
        {
            ViewData["IdDonHang"] = new SelectList(_context.DonHangs, "IdDonHang", "IdDonHang");
            ViewData["IdSanPham"] = new SelectList(_context.SanPhams, "IdSanPham", "IdSanPham");
            return View();
        }

        // POST: ChiTietDonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdChiTietDonHang,IdDonHang,IdSanPham,SoLuong,GiaBan")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietDonHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDonHang"] = new SelectList(_context.DonHangs, "IdDonHang", "IdDonHang", chiTietDonHang.IdDonHang);
            ViewData["IdSanPham"] = new SelectList(_context.SanPhams, "IdSanPham", "IdSanPham", chiTietDonHang.IdSanPham);
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHangs.FindAsync(id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }
            ViewData["IdDonHang"] = new SelectList(_context.DonHangs, "IdDonHang", "IdDonHang", chiTietDonHang.IdDonHang);
            ViewData["IdSanPham"] = new SelectList(_context.SanPhams, "IdSanPham", "IdSanPham", chiTietDonHang.IdSanPham);
            return View(chiTietDonHang);
        }

        // POST: ChiTietDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdChiTietDonHang,IdDonHang,IdSanPham,SoLuong,GiaBan")] ChiTietDonHang chiTietDonHang)
        {
            if (id != chiTietDonHang.IdChiTietDonHang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietDonHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietDonHangExists(chiTietDonHang.IdChiTietDonHang))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDonHang"] = new SelectList(_context.DonHangs, "IdDonHang", "IdDonHang", chiTietDonHang.IdDonHang);
            ViewData["IdSanPham"] = new SelectList(_context.SanPhams, "IdSanPham", "IdSanPham", chiTietDonHang.IdSanPham);
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHangs
                .Include(c => c.IdDonHangNavigation)
                .Include(c => c.IdSanPhamNavigation)
                .FirstOrDefaultAsync(m => m.IdChiTietDonHang == id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            return View(chiTietDonHang);
        }

        // POST: ChiTietDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietDonHang = await _context.ChiTietDonHangs.FindAsync(id);
            if (chiTietDonHang != null)
            {
                _context.ChiTietDonHangs.Remove(chiTietDonHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> AddProductToCart(int idSanPham, int quantity)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Nếu người dùng chưa đăng nhập, redirect đến trang đăng nhập với thông báo
                TempData["Message"] = "Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng.";
                return RedirectToAction("Index", "DangNhap"); // Điều chỉnh theo tên controller và action của trang đăng nhập
            }
            int customerId = GetCurrentCustomerId();

            // Tìm giỏ hàng của khách hàng hiện tại
            var cart = await _context.DonHangs.FirstOrDefaultAsync(c => c.IdUser == customerId);

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

            // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            var cartDetail = await _context.ChiTietDonHangs
                .FirstOrDefaultAsync(cd => cd.IdDonHang == cart.IdDonHang && cd.IdSanPham == idSanPham);

            if (cartDetail != null)
            {
                // Nếu sản phẩm đã có, tăng số lượng
                cartDetail.SoLuong += quantity;
            }
            else
            {
                // Nếu sản phẩm chưa có, thêm mới vào giỏ hàng
                var product = await _context.SanPhams.FindAsync(idSanPham);
                if (product != null)
                {
                    cartDetail = new ChiTietDonHang
                    {
                        IdDonHang = cart.IdDonHang,
                        IdSanPham = idSanPham,
                        SoLuong = quantity,
                        GiaBan = product.Gia
                    };
                    _context.ChiTietDonHangs.Add(cartDetail);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // Chuyển hướng về trang giỏ hàng
        }

        // Phương thức lấy CustomerId của người dùng hiện tại
        private int GetCurrentCustomerId()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var customer = _context.Users.SingleOrDefault(c => c.Email == email);
            return customer?.IdUser ?? 0; // Trả về 0 nếu không tìm thấy customer
        }

        private bool ChiTietDonHangExists(int id)
        {
            return _context.ChiTietDonHangs.Any(e => e.IdChiTietDonHang == id);
        }
    }
}
