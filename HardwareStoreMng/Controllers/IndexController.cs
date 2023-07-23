using HardwareStoreMng.CashServices;
using HardwareStoreMng.DTO;
using HardwareStoreMng.Models;
using HardwareStoreMng.Repositories.BarCodesRepository;
using HardwareStoreMng.Repositories.CustomerRepository;
using HardwareStoreMng.Repositories.EmployeeRepository;
using HardwareStoreMng.Repositories.InvoiceItemRepository;
using HardwareStoreMng.Repositories.InvoiceRepository;
using HardwareStoreMng.Repositories.PorductRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Serilog;

namespace HardwareStoreMng.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        public readonly ProductInterface _productInterface;
        public readonly CustomerInterface _customerInterface;
        public readonly EmployeeInterface _employeeInterface;
        public readonly InvoiceInterface  _invoiceInterface;
        public readonly InvoiceItemInterface _invoiceItemInterface;
        public readonly BarCodesInterface _barCodesInterface;
        private readonly IredisCashServices _cacheService;

        public IndexController(ProductInterface productInterface, CustomerInterface customerInterface,
          EmployeeInterface employeeInterface, InvoiceInterface invoiceInterface, InvoiceItemInterface invoiceItemInterface,
         BarCodesInterface barCodesInterface, IredisCashServices iredisCashServices)
        {
            _productInterface = productInterface;
            _customerInterface= customerInterface;
            _employeeInterface= employeeInterface;
            _invoiceInterface= invoiceInterface;
            _invoiceItemInterface= invoiceItemInterface;
            _barCodesInterface= barCodesInterface;
            _cacheService = iredisCashServices;
        }
        /// <summary>
        /// Action to Retrieve All Products .
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GettAllProduct()
        {
            Log.Information("Run GettAllProduct Action");
            var productDtos = _productInterface.GetAllProducts();
            Log.Information("Retrieve All Products sucsessfully");
            return Ok(productDtos);
            

        }
        /// <summary>
        /// Action to Retrieve Product By Product ID .
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GettProductById( int productid)
        {
            Log.Information("Run GettProductById Action");
            var productDto = _productInterface.GetProductById(productid);

            if (productDto == null)
            {
                return NotFound(); 
            }
            Log.Information("Retrieve Product By Product ID Sucsessfully");

            return Ok(productDto);
        }
        /// <summary>
        /// Action to Add New Product .
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/IndexController/AddNewProduct
        ///     {        
        ///       "productId": 0,
        ///       "productName": "Screen",
        ///       "productDescription": "Screen for IPhone 13 Pro",
        ///       "price": 400       
        ///     }
        /// </remarks>6
        [HttpPost]
        [Route("[action]")]
        public IActionResult AddNewProduct([FromBody]ProductDTO productDTO)
        {
            Log.Information("Run AddNewProduct ");  
            _productInterface.AddProduct(productDTO);

            Log.Information("Product Added ");

            return Ok("Product Added");
        }
        /// <summary>
        /// Action to Add Update Product Price Of An Existing Product .
        /// </summary>
        /// /// <remarks>
        /// /// Sample request:
        /// 
        ///     POST api/IndexController/UpdateProduct
        ///     {        
        ///       "productId": 6,
        ///       "productName": "string",
        ///       "productDescription": "string",
        ///       "price": 500      
        ///     }
        /// </remarks>6
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateProduct(int productid, [FromBody] ProductDTO productDto)
        {
            Log.Information("Run UpdateProduct Action ");
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var existingProduct = _productInterface.GetProductById(productid);

                    if (existingProduct == null)
                    {
                        return NotFound();
                    }


                    existingProduct.price = productDto.price;

                    _productInterface.UpdateProduct(existingProduct);
                    return Ok("Price Updated");
                }

                return BadRequest(ModelState);

            }
            Log.Information("UpdateProduct Sucsessfully ");

            return Ok();
        }
        /// <summary>
        /// Action to Delete An Existing Product .
        /// </summary>
        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteProduct (int productid)
        {
            Log.Information("Run DeleteProduct Action");
            var existingProduct = _productInterface.GetProductById(productid);

            if (existingProduct == null)
            {
                return NotFound();
            }

            _productInterface.DeleteProduct(productid);
            Log.Information("Product Deleted ");

            return Ok("Product Deleted");



        }
        /// <summary>
        /// Action to Retrieve All Customers .
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GettAllCustomers()
        {
            Log.Information("Run GettAllCustomers Action");
            var customerDtos = _customerInterface.GetAllCustomers();
            Log.Information("Retrieve All Customers Sucsessfully");

            return Ok(customerDtos);

        }
        /// <summary>
        /// Action to Retrieve Customer By ID .
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GettCustomerById(int customerid)
        {
            Log.Information("Run GettCustomerById Action");
            var customerDto = _customerInterface.GetCustomerById(customerid);

            if (customerDto == null)
            {
                Log.Error("Customer Not Found");
                return NotFound(); 
            }
            Log.Error("Retrieve Customer By ID Sucsessfully");

            return Ok(customerDto);
        }
        /// <summary>
        /// Action to Add New Customer And Add Customer Name To Redis Server .
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/IndexCntroller/AddNewCustomer
        ///     {        
        ///       "customerId": 0,
        ///      "customerName": "Mohammad Maaitah",
       ///       "customerEmail": "Mohammad@gmail.com",
       ///        "customerPhone": "0798162772"        
        ///     }
        /// </remarks>6
        [HttpPost]
        [Route("[action]")]
        public IActionResult AddNewCustomer([FromBody] CustomerDTO customerDTO)
        {
            Log.Information("Run AddNewCustomer Action");
            _customerInterface.AddCustomer(customerDTO);
            var cacheData = _cacheService.SetData<string>("TempKey", customerDTO.CustomerName, DateTime.Now.AddMinutes(1));
            if (cacheData)
            {
                Log.Information(" Customer Added And Set Redis Server Success");
                return Ok("Customer Added And Set Redis Server Success");
            }
            Log.Error("No Customer Added And No Data In Redis Has been Added");
            return BadRequest("No Customer Added And No Data In Redis Has been Added");

            
            
        }
        /// <summary>
        /// Action to Update Email And Phone Of An Existing Customer .
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/IndexCntroller/UpdateCustomer
        ///     {        
        ///       "customerId": 5,
        ///      "customerName": "string",
        ///       "customerEmail": "string",
        ///        "customerPhone": "0788162772"        
        ///     }
        /// </remarks>6
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateCustomer(int customertid,[FromBody] CustomerDTO customerDto)
        {
            Log.Information("Run UpdateCustomer Action ");
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    

                    var existingCustomer = _customerInterface.GetCustomerById(customertid);

                    if (existingCustomer == null)
                    {
                        Log.Error("Customer Not Found");
                        return NotFound();
                    }
                    existingCustomer.CustomerEmail = customerDto.CustomerEmail;
                    existingCustomer.CustomerPhone= customerDto.CustomerPhone;
                    _customerInterface.UpdateCustomer(existingCustomer);
                    Log.Information("Update Customer Phone Sucsessfully");
                    return Ok("CustomerUpdated");
                }

                return BadRequest(ModelState);
            }
            return Ok();
        }
        /// <summary>
        /// Action to Delete An Existing Customer .
        /// </summary>

        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteCustomer(int customertid)
        {
            Log.Information("Run DeleteCustomer Action ");
            var existingCustomer = _customerInterface.GetCustomerById(customertid);

            if (existingCustomer == null)
            {
                Log.Error("Customer Not Found");
                return NotFound();
            }

            _customerInterface.DeleteCustomer(customertid);
            Log.Information("Customer Deleted ");

            return Ok("Customer Deleted");
        }
        /// <summary>
        /// Action to Retrieve All Employees .
        /// </summary>

        [HttpGet]
        [Route("[action]")]
        public IActionResult GettAllEmployees()
        {
            Log.Information("Run GettAllEmployees Action ");
            var employeeDtos = _employeeInterface.GetAllEmployees();
            Log.Information("Retrieve All Employees Sucsessfully");
            return Ok(employeeDtos);

        }
        /// <summary>
        /// Action to Retrieve Employee By ID .
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GettEmployeeById(int employeeid)
        {
            Log.Information("Run GettEmployeeById Action ");
            var employeeDto = _employeeInterface.GetEmployeeById(employeeid);

            if (employeeDto == null)
            {
                Log.Error("Employye Not Found");
                return NotFound();
            }
            Log.Information("GettEmployeeById Sucsessfully ");

            return Ok(employeeDto);
        }
        /// <summary>
        /// Action to Add New Employee .
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Employee/IndesxController/AddNewEmployee
        ///     {        
        ///      "employeeId": 0,
        ///       "employeeName": "Siraj Maaitah",
        ///      "employeePosetion": "General Maneger"       
        ///     }
        /// </remarks>6
        [HttpPost]
        [Route("[action]")]
        public IActionResult AddNewEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            Log.Information("Run AddNewEmployee Action ");
            _employeeInterface.AddEmployee(employeeDTO);

            Log.Information("Employee Added ");

            return Ok("Employee Added");
        }
        /// <summary>
        /// Action to Update Posetion Of An Existnig Employee.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/IndexController/UpdateEmployee
        ///     {        
        ///      "employeeId": 5,
        ///       "employeeName": "String",
        ///      "employeePosetion": "Resiption Employee"       
        ///     }
        /// </remarks>6
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateEmployee(int Employeeid, [FromBody]EmployeeDTO employeeDto)
        {
            Log.Information("Run UpdateEmployee Action");
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var existingEmployee = _employeeInterface.GetEmployeeById(Employeeid);

                    if (existingEmployee == null)
                    {
                        Log.Error("Employee Not Found");
                        return NotFound();
                    }

                    existingEmployee.EmployeePosetion = employeeDto.EmployeePosetion;

                    _employeeInterface.UpdateEmployee(existingEmployee);
                    Log.Information("Employee posetion Updated");
                    return Ok("Employee posetion Updated");
                }

                return BadRequest(ModelState);
            }
            return Ok();
        }
        /// <summary>
        /// Action to Delete An Existing Employee .
        /// </summary>
        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteEmployee(int employeetid)
        {
            Log.Information("Run DeleteEmployee Action ");
            var existingEmployeet= _employeeInterface.GetEmployeeById(employeetid);

            if (existingEmployeet == null)
            {
                Log.Error("Employee Not Found");
                return NotFound();
            }

            _employeeInterface.DeleteEmployee(employeetid);
            Log.Information("Employee Deleted ");

            return Ok("Employee Deleted");
        }
        /// <summary>
        /// Action to Retrieve All Invoices .
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GettAllInvoices()
        {
            Log.Information("Run GettAllInvoices Action");
            var invoiceDtos = _invoiceInterface.GetAllInvoices();
            Log.Information("Retrieve All Invoices Sucsessfully");

            return Ok(invoiceDtos);

        }
        /// <summary>
        /// Action to Retrieve Invoice By ID.
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GettInvoiceById(int invoiceid)
        {
            Log.Information("Run GettInvoiceById Action");
            var invoiceDto = _invoiceInterface.GetInvoiceById(invoiceid);

            if (invoiceDto == null)
            {
                Log.Error("Invoice Not Found");
                return NotFound();
            }
            Log.Information("Retrieve Invoice By ID Sucessfully");
            return Ok(invoiceDto);
        }
        /// <summary>
        /// Action to Add New Invoice .
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/IndexController/AddNewInvoice
        ///     {        
        ///      "invoiceId": 0,
       ///       "invoiceNumber": 4789,
       ///       "totalPrice": 60,
       ///        "date": "2023-05-04",
       ///        "customerId": 5,
        ///       "employeeId": 4       
        ///     }
        /// </remarks>6
        [HttpPost]
        [Route("[action]")]
        public IActionResult AddNewInvoice([FromBody] InvoiceDTO invoiceDTO)
        {
            Log.Information("Run AddNewInvoice Action  ");
            _invoiceInterface.AddNewInvoice(invoiceDTO);
            Log.Information("Invoice Added  ");
            return Ok("Invoice Added");
        }
        /// <summary>
        /// Action to Update Total Price Of An Existing Invoice .
        /// </summary>
        /// /// /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/IndexController/UpdateInvoice
        ///     {        
        ///     "invoiceId": 5,
        ///      "invoiceNumber": 0,
        ///     "totalPrice": 100,
        ///     "date": "2023-07-22T14:11:56.327Z",
        ///     "customerId": 0,
       ///      "employeeId": 0     
        ///     }
        /// </remarks>6
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateInvoice(int Invoiceid, [FromBody] InvoiceDTO InvoiceDto)
        {
            Log.Information("Run UpdateInvoice Action");
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    
                    var existingInvoice = _invoiceInterface.GetInvoiceById(Invoiceid);

                    if (existingInvoice == null)
                    {
                        Log.Error("Invoice Not Found");
                        return NotFound();
                    }

                    existingInvoice.TotalPrice = InvoiceDto.TotalPrice;

                    _invoiceInterface.UpdateInvoice(existingInvoice);
                    Log.Information("Update Total Price Of An Existing Invoice Sucsessfully");
                    return Ok("Invoice TotalPrice Updated");
                }

                return BadRequest(ModelState);
            }
            return Ok();
        }
        /// <summary>
        /// Action to Delete An Existing Invoice .
        /// </summary>
        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteInvoice(int invoiceid)
        {
            Log.Information("Run DeleteInvoice Action ");
            var existingInvoice = _invoiceInterface.GetInvoiceById(invoiceid);

            if (existingInvoice == null)
            {
                Log.Error("Invoice Not Found");
                return NotFound();
            }

            _invoiceInterface.DeleteInvoice(invoiceid);
            Log.Information("Invoice Deleted ");
            return Ok("Invoice Deleted");
        }
        /// <summary>
        /// Action to Retrieve All InvoiceItem .
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GettAllInvoiceItem()
        {
            Log.Information("Run GettAllInvoiceItem Action");
            var invoiceItemDtos = _invoiceItemInterface.GetAllInvoiceItem();
            Log.Information("Retrieve All InvoiceItem Sucsessfully");
            return Ok(invoiceItemDtos);


        }
        /// <summary>
        /// Action to Retrieve InvoiceItem By ID .
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GettInvoiceItemById(int invoiceid)
        {
            Log.Information("Run GettInvoiceItemById Action");
            var invoiceitemDto = _invoiceItemInterface.GetInvoiceItemtById(invoiceid);

            if (invoiceitemDto == null)
            {
                Log.Error("InoiceItem ot Found");
                return NotFound();
            }
            Log.Information("Retrieve InvoiceItem By ID Successfully");
            return Ok(invoiceitemDto);
        }
        /// <summary>
        /// Action to Add New InvoiceItem .
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/IndexController/AddNewInvoiceItem
        ///     {        
        ///       "invoiceItemId": 0,
        ///       "invoiceId": 5,
        ///        "productId": 3,
        ///        "quantity": 4     
        ///     }
        /// </remarks>6
        [HttpPost]
        [Route("[action]")]
        public IActionResult AddNewInvoiceItem([FromBody] InvoiceItemDTO invoiceitemDTO)
        {
            Log.Information("Run AddNewInvoiceItem Action");
            _invoiceItemInterface.AddInvoiceItem(invoiceitemDTO);
            Log.Information("InvoiceItem Added");
            return Ok("InvoiceItem Added");
        }
        /// <summary>
        /// Action to Update Quantity Of An Existing IncoiceItem.
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/IndexController/UpdateInvoiceItam
        ///     {        
        ///       "invoiceItemId": 0,
        ///       "invoiceId": 5,
        ///       "productId": 2,
        ///       "quantity": 3    
        ///     }
        /// </remarks>6
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateInvoiceItam([FromBody] InvoiceItemDTO InvoicItameDto)
        {
            Log.Information("Run UpdateInvoiceItam Action ");
            if (ModelState.IsValid)
            {
                _invoiceItemInterface.UpdateInvoiceItem(InvoicItameDto);
                Log.Information("Quantity Is Updated");
                return new ObjectResult("InvoiceItem Updated") { StatusCode = 200 };
            }
            Log.Error("Update Quantity Is Not Successfully");
            return BadRequest(ModelState);
        }
        /// <summary>
        /// Action to Delete An Existing InvoiceItem .
        /// </summary>
        //[HttpPut]
        //[Route("[action]")]
        //public IActionResult UpdateInvoiceItam(int invoiceId)
        //{
        //    var invoiceitems = _invoiceItemInterface.GetInvoiceItemsByInvoiceId(invoiceId);

        //    if (invoiceitems == null || !invoiceitems.Any())
        //    {
        //        return NotFound();
        //    }

        //    return Ok(invoiceitems);
        //}
        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteInvoiceItem(int invoiceid,int productid)
        {
            Log.Information("Run DeleteInvoiceItem Action");
            _invoiceItemInterface.DeleteInvoiceItem(invoiceid, productid);
            Log.Information("Invoice item deleted successfully");
            return Ok("Invoice item deleted successfully.");
        }
        /// <summary>
        /// Action to Retrieve All BarCodes .
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GettAllBarCodes()
        {
            Log.Information("Run GettAllBarCodes Action ");
            var barcodesDtos = _barCodesInterface.GetAllBarCodes();
            Log.Information("Retrieve All BarCodes Successfully");
            return Ok(barcodesDtos);
        }
        /// <summary>
        /// Action to Retrieve BarCodes By ID .
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GettBarcodesId(int productid)
        {
            Log.Information("Run GettBarcodesId Action ");
            var barcodesDto = _barCodesInterface.GetBarcodeById(productid);

            if (barcodesDto == null)
            {
                Log.Error("BarCode Not Found");
                return NotFound();
            }
            Log.Information("Retrieve BarCodes By ID Successfully ");
            return Ok(barcodesDto);
        }
        /// <summary>
        /// Action to Add New BarCode For Product .
        /// </summary>
        /// ///  <remarks>
        /// Sample request:
        /// 
        ///     POST api/IndexController/AddNewBarcode
        ///     {        
        ///       "barCodeId": 0,
        ///       "productId": 5,
        ///       "barCodeName": "5dfh2hfx"
        ///     }
        /// </remarks>6
        [HttpPost]
        [Route("[action]")]
        public IActionResult AddNewBarcode([FromBody] BarCodesDTO barcodeDTO)
        {
            Log.Information("Run AddNewBarcode Action ");
            _barCodesInterface.AddBarCode(barcodeDTO);
            Log.Information("BarCode Added");
            return Ok("BarCode Added");
        }
        /// <summary>
        /// Action to Update BarCodeName Of An Existing BarCode .
        /// </summary>
        /// ///   <remarks>
        /// Sample request:
        /// 
        ///     POST api/IndexController/UpdateBarcode
        ///     {        
        ///       "barCodeId": 3,
        ///       "productId": 0,
        ///       "barCodeName": "88dhfsd"
        ///     }
        /// </remarks>6
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateBarcode([FromBody] BarCodesDTO BarCodesDto)
        {
            Log.Information("Run UpdateBarcode Action");
            if (ModelState.IsValid)
            {
                _barCodesInterface.UpdateBarcode(BarCodesDto);
                Log.Information("BarCode Updated");
                return new ObjectResult("BarCode Updated") { StatusCode = 200 };
            }
            Log.Error("BarCode Not Found");
            return BadRequest(ModelState);
        }
        /// <summary>
        /// Action to Delete An Existing BarCode .
        /// </summary>
        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteBarCode(int barcodeid)
        {
            Log.Information("Run DeleteBarCode Action ");
            var existingBarCode = _barCodesInterface.GetBarcodeById(barcodeid);

            if (existingBarCode == null)
            {
                Log.Error("BarCode Not Found");
                return NotFound();
            }

            _barCodesInterface.DeleteBarCode(barcodeid);
            Log.Information("BarCode Deleted");
            return Ok("BarCode Deleted");
        }
        /// <summary>
        /// Action to Retrieve The Value That Saved In Redis Server .
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetValueFromRedisServer()
        {
            Log.Information("Run GetValueFromRedisServer Action");
            var cacheData = _cacheService.GetData<string>("TempKey");
            if (cacheData != null)
            {
                Log.Information("Retrieve The Value That Saved In Redis Server Successfully");
                return Ok(cacheData);
            }
            Log.Error("Redis Server Is Empty");
            return BadRequest("No Data In Redis");
        }
        /// <summary>
        /// Action to Delete An Existing Value From Redis Server .
        /// </summary>
        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteValueFromRedisServer()
        {
            Log.Information("Run DeleteValueFromRedisServer Action ");
            var cacheData = _cacheService.RemoveData("TempKey");
            if (cacheData != null)
            {
                Log.Information("Delete An Existing Value From Redis Server Successfully");
                return Ok("Removed Success");
            }
            Log.Error("No Data In Redis Has been Removed");
            return BadRequest("No Data In Redis Has been Removed");
        }
    }
}