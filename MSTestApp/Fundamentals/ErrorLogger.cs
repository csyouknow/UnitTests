using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTestApp.Fundamentals
{
    class ErrorLogger
    {

        public string LastError { get; set; }

        public event EventHandler<Guid> ErrorLogged;

        private Guid _errorId;

        public void Log(string error)
        {
            if (String.IsNullOrWhiteSpace(error))
            {
                throw new ArgumentNullException();
            }

            LastError = error;

            _errorId = Guid.NewGuid();
            OnErrorLogged();
        }
        
        protected virtual void OnErrorLogged()
        {
            ErrorLogged?.Invoke(this, _errorId);
        }
    }
}
