using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using WebApplication1.Models;
using WebApplication1.Models.ViewModel;

namespace WebApplication1.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public IActionResult Index(int page = 1, int pageSize = 3)
        {
            var totalCount = _context.Customers.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var customers = _context.Customers.Include(c=>c.CustomerType)
                .OrderBy(c => c.CustomerId) // Sort by Customer ID
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var startItem = (page - 1) * pageSize + 1;
            var endItem = Math.Min(page * pageSize, totalCount);

            var viewModel = new CustomerIndexViewModel
            {
                Customers = customers,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                StartItem = startItem,
                EndItem = endItem
            };

            return View(viewModel);
        }


        // GET: Customers/Details/5
        public IActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.DeliveryInfoList)
                .Include(c => c.CustomerType)
                .FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        // GET: Customers/Create
        public IActionResult Create()
        {
            var customerCount = _context.Customers.Count();
            var nextCustomerId = (customerCount + 1).ToString("D3"); // Format as "001", "002", etc.

            var viewModel = new CustomerCreateViewModel
            {
                CustomerId = nextCustomerId, // Pre-generated ID
                CustomerTypes = _context.CustomerTypes.ToList()
            };
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Create(CustomerCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.CustomerTypes = _context.CustomerTypes.ToList();
                return View(viewModel);
            }

            var customer = new Customer
            {
                CustomerId = (viewModel.CustomerId), // Use generated ID
                Name = viewModel.Name,
                Address = viewModel.Address,
                CustomerTypeId = viewModel.CustomerTypeId,
                BusinessStartDate = viewModel.BusinessStartDate,
                Phone = viewModel.Phone,
                CreditLimit = viewModel.CreditLimit,
                DeliveryInfoList = viewModel.DeliveryInfos.Select(d => new DeliveryInfo
                {
                    DeliveryAddress = d.DeliveryAddress,
                    ContactPerson = d.ContactPerson,
                    Phone = d.Phone
                }).ToList()

            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult CreateCustomerType(CustomerType customerType)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid data");

            _context.CustomerTypes.Add(customerType);
            _context.SaveChanges();

            return Json(customerType);
        }


        // GET: Customers/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers
                .Include(c => c.DeliveryInfoList) // Include related DeliveryInfo entities
                .FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            ViewBag.CustomerTypes=new SelectList(_context.CustomerTypes, "CustomerTypeId", "CustomerTypeName",customer.CustomerTypeId);

            var viewModel = new CustomerCreateViewModel
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Address = customer.Address,
                CustomerTypeId = customer.CustomerTypeId,
                BusinessStartDate = customer.BusinessStartDate,
                Phone = customer.Phone,
                CreditLimit = customer.CreditLimit,
                DeliveryInfos = customer.DeliveryInfoList.Select(d => new DeliveryInfoViewModel
                {
                    DeliveryAddress = d.DeliveryAddress,
                    ContactPerson = d.ContactPerson,
                    Phone = d.Phone
                }).ToList(),
                CustomerTypes = _context.CustomerTypes.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CustomerCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.CustomerTypes = _context.CustomerTypes.ToList();
                return View(viewModel);
            }

            var customer = _context.Customers
                .Include(c => c.DeliveryInfoList)
                .FirstOrDefault(c => c.CustomerId == viewModel.CustomerId);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = viewModel.Name;
            customer.Address = viewModel.Address;
            customer.CustomerTypeId = viewModel.CustomerTypeId;
            customer.BusinessStartDate = viewModel.BusinessStartDate;
            customer.Phone = viewModel.Phone;
            customer.CreditLimit = viewModel.CreditLimit;

            // Update DeliveryInfos
            customer.DeliveryInfoList.Clear();
            customer.DeliveryInfoList.AddRange(viewModel.DeliveryInfos.Select(d => new DeliveryInfo
            {
                DeliveryAddress = d.DeliveryAddress,
                ContactPerson = d.ContactPerson,
                Phone = d.Phone
            }));

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.CustomerType)
                //.Include(c => c.DeliveryInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        

    }
}
