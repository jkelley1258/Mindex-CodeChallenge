﻿using System;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public String CompensationId { get; set; }

        public String EmployeeId { get; set; }

        public double Salary { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
