using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BLApi;
using BO;

namespace UI //dont need
{
    //public class CompanySchedule : DependencyObject
    //{
    //    static readonly DependencyProperty schedule = DependencyProperty.Register("ScheduleOfRoute", typeof(BO.ScheduleOfRoute), typeof(CompanySchedule));

    //    public ObservableCollection<ScheduleOfRoute> companySchedule { get; } = new ObservableCollection<ScheduleOfRoute>();
    //}

    public class CompanySchedule
    {
        //private List<ScheduleOfRoute> companySchedule;

        //public List<ScheduleOfRoute> CompleteCompanySchedule
        //{
        //    get { return companySchedule; }
        //    set { companySchedule = value; }
        //}

        private ScheduleOfRoute schedule;

        public ScheduleOfRoute compSchedule
        {
            get { return schedule; }
            set { schedule = value; }
        }


    }
}
