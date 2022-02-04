using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.SamplePages
{
    public class BasicsModel : PageModel
    {
        //data fields
        public string MyName;

        //properties
        // the annotation [TempData] stores data until its read in another immediate request
        //this annotation attribute has two method called Keep(string) and Peek(string) (used on Content Page)
        //keep int a dictionary(name/value pair)
        //useful to redirect when data is required for more than a single request
        //Implemented by TempData providers using either cookies or session state
        //TempData is NOT bount to any particular control like BindProperty
        [TempData]
        public string FeedBack { get; set; }


        
        //the annotation BindProperty ties a property in the PageModel class
        // directly to a control on the Content Page
        //data is transferred between the twoo automatically
        //on the Content page, the control to use this property will have a helper-tag called asp-for

        //to retain a value in the control tied to this property and retained
        //via the @page use the SupportGet attribute = true
        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }

        //constructors
        public void OnGet()
        {
            //executes in response to a Get Request from the browser
            //when the page is "first" accessed

            Random rnd = new Random();
            int oddeven = rnd.Next(0,25);
            if(oddeven % 2 == 0)
            {
                MyName = $"Don is even {oddeven}";
            }
            else
            {
                MyName = null;
            }
        }

        //processing in response to a request from a form on a web page
        //this request is referred to as a Post (method="post")

        //General post
        //Page()
        // does NOT issue a OnGet request
        //remains on the current page
        // a good action for form processing involving validation and with the catch of a try/catch
        //RedirectToPage()
        //  DOES isse a Onget request
        //  is used to retaining input values via the @page

        public IActionResult OnPost()
        {
            //thi sline of code is used to cause a delay in processing
            // so we can see on the Network Activity some type of simulated processing
            Thread.Sleep(2000);

            //retrieve data via the Request object
            //Request: web page to server
            //Response: server to web page
            string buttonvalue = Request.Form["theButton"];
            FeedBack = $"Button pressed is {buttonvalue} with numeric input of {id}";

            //return Page();// does not issue a OnGet()
            return RedirectToPage(new {id = id});//request for OnGet()
        }
    }
}
