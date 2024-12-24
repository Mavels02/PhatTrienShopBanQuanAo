using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assignment.Controllers
{
    
    public class SanPhamsController : Controller
    {
        private readonly AsmC4Context _context;

        public SanPhamsController(AsmC4Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> AddToCart(int idSanPham)
        {
            // Lấy thông tin sản phẩm
            // Lấy thông tin sản phẩm
            var sanPham = await _context.SanPhams.FindAsync(idSanPham);
            if (sanPham == null)
            {
                return NotFound();
            }

            // Trả về view với thông tin sản phẩm
            return View(sanPham);
        }
        // GET: SanPhams
        public async Task<IActionResult> Index()
        {
            var asmC4Context = _context.SanPhams.Include(s => s.IdCategoryNavigation);
            return View(await asmC4Context.ToListAsync());
        }

        // GET: SanPhams/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.IdCategoryNavigation)
                .FirstOrDefaultAsync(m => m.IdSanPham == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPhams/Create
        public IActionResult Create()
        {
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSanPham,TenSanPham,MoTa,Gia,SoLuong,HinhAnh,IdCategory")] SanPham sanPham, IFormFile? HinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (HinhAnh != null && HinhAnh.Length > 0)
                {
                    // Chọn thư mục tùy chỉnh để lưu trữ hình ảnh
                    var folderPath = Path.Combine("wwwroot", "Images");

                    // Tạo thư mục nếu chưa tồn tại
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Tạo tên tệp và đường dẫn tệp
                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    var filePath = Path.Combine(folderPath, fileName);

                    // Lưu tệp vào thư mục
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await HinhAnh.CopyToAsync(stream);
                    }

                    // Lưu đường dẫn tương đối vào cơ sở dữ liệu
                    sanPham.HinhAnh = Path.Combine("Images", fileName);
                }
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", sanPham.IdCategory);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", sanPham.IdCategory);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSanPham,TenSanPham,MoTa,Gia,SoLuong,HinhAnh,IdCategory")] SanPham sanPham, IFormFile? HinhAnh)
        {
            if (id != sanPham.IdSanPham)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnh != null && HinhAnh.Length > 0)
                    {
                        // Chọn thư mục tùy chỉnh để lưu trữ hình ảnh
                        var folderPath = Path.Combine("wwwroot", "Images");

                        // Tạo thư mục nếu chưa tồn tại
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // Tạo tên tệp và đường dẫn tệp
                        var fileName = Path.GetFileName(HinhAnh.FileName);
                        var filePath = Path.Combine(folderPath, fileName);

                        // Lưu tệp vào thư mục
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await HinhAnh.CopyToAsync(stream);
                        }

                        // Cập nhật đường dẫn hình ảnh vào cơ sở dữ liệu
                        sanPham.HinhAnh = Path.Combine("Images", fileName);
                    }
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.IdSanPham))
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
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", sanPham.IdCategory);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.IdCategoryNavigation)
                .FirstOrDefaultAsync(m => m.IdSanPham == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.IdSanPham == id);
        }
    }
}
