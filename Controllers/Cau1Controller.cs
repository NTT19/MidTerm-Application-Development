using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MidTerm.Entity;
using MidTerm.ViewModel;
namespace MidTerm.Controllers
{
    public class Cau1Controller : Controller
    {
        private readonly QlbanHangContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Cau1Controller(QlbanHangContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Create(SanPhamViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.Hinh != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "HinhAnh/SanPham");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Hinh.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Hinh.CopyToAsync(fileStream);
                    }
                }

                SanPham newSanPham = new SanPham
                {
                    TenSp = model.TenSp,
                    DonViTinh = model.DonViTinh,
                    DonGia = model.DonGia,
                    Hinh = uniqueFileName
                };

                _context.SanPhams.Add(newSanPham);
                await _context.SaveChangesAsync();

                return RedirectToAction("View");
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult View()
        {
            var data = _context.SanPhams != null ? _context.SanPhams.ToList() : new List<SanPham>();
            return View(data);
        }


    }
}
