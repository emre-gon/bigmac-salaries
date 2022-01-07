﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public abstract class ITaxSystem
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Three letter code</returns>
        public abstract string CountryCode();


        public abstract decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date);



        public WageModel GetAnnualNetWage(WageModel input, DateTime? date = null)
        {
            if (date == null)
                date = DateTime.Now.Date;

            var annualWage = GetAnnualWage(input);

            annualWage.NetLocalPrice = CalculateAnnualNetWage(annualWage.GrossLocalPrice, date.Value);


            return annualWage;
        }

        public WageModel GetMonthlyNetWage(WageModel input, DateTime? date = null)
        {
            return GetMonthlyWage(GetAnnualNetWage(input));
        }



        public WageModel GetAnnualWage(WageModel input)
        {
            if (input.Type == Domain.MinWageType.Annually)
                return input;


            WageModel tbr = new WageModel()
            {
                Type = Domain.MinWageType.Annually,
                Profession = input.Profession,
                Date = input.Date,
                TaxWedge = input.TaxWedge
            };

            if (input.Type == Domain.MinWageType.Monthly)
            {
                tbr.GrossLocalPrice = input.GrossLocalPrice * 12;
                tbr.NetLocalPrice = input.NetLocalPrice * 12;
            }


            if (input.Type == Domain.MinWageType.Hourly)
            {
                tbr.GrossLocalPrice = input.GrossLocalPrice * 1950;
                tbr.NetLocalPrice = input.NetLocalPrice * 1950;
            }

            return tbr;
        }

        public WageModel GetMonthlyWage(WageModel input)
        {
            var wage = GetAnnualWage(input);

            wage.GrossLocalPrice /= 12;
            wage.NetLocalPrice /= 12;

            return wage;
        }
    }
}