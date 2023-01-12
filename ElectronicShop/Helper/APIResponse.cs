using System;

namespace ElectronicShop.Helper
{
    public class APIResponse<T>
    {
        private T data { get; set; } = default(T)!;
        private bool success { get; set; } = false; //by default it is false
        private string? message { get; set; }
        private int totalCounts { get; set; } = 0; //This property will be used when doing pagination because sometimes we filtered the values and we have to know how much records we have

        public T Data
        {
            get { return data; }
            set { data = value; }
        }
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }
        public string? Message
        {
            get { return message; }
            set { message = value; }
        }
        public int TotalCounts
        {
            get { return totalCounts; }
            set { totalCounts = value; }
        }

        public void OperationCompleted()
        {
            this.Success = true;
            this.Message = "Operation Completed";
        }

        public void OperationFailed(Exception ex)
        {
            this.Data = default(T)!; //returns null
            this.Success = false;
            this.Message = ex.Message;
        }

        public void NoData()
        {
            this.Data = default(T)!;
            this.Success = false;
            this.Message = "Operation Completed but no data available";
        }

        public void NotExist()
        {
            this.Success = false;
            this.Message = "This ID doesn't exists";
        }

        public void SuccessTrue()
        {
            this.Success = true;
        }

        public void CheckGetAll(int count) //Checks if there is data or not in the database
        {
            if (count > 0) this.OperationCompleted();
            else this.NoData();
        }
    }
}