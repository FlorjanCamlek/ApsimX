﻿using Models.Core;
using Models.CLEM.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Models.CLEM;
using Models.CLEM.Groupings;
using System.ComponentModel.DataAnnotations;
using Models.Core.Attributes;

namespace Models.CLEM.Activities
{
    /// <summary>Labour activity task</summary>
    /// <summary>Defines a labour activity task with associated costs</summary>
    [Serializable]
    [ViewName("UserInterface.Views.GridView")]
    [PresenterName("UserInterface.Presenters.PropertyPresenter")]
    [ValidParent(ParentType = typeof(CLEMActivityBase))]
    [ValidParent(ParentType = typeof(ActivitiesHolder))]
    [ValidParent(ParentType = typeof(ActivityFolder))]
    [Description("This activity will arange payment for a task based on the labour specified in the labour requirement.")]
    [Version(1, 0, 1, "Adam Liedloff", "CSIRO", "")]
    public class LabourActivityTask : CLEMActivityBase, IValidatableObject
    {
        ///// <summary>
        ///// Amount payable
        ///// </summary>
        //[Description("Amount payable")]
        //[Required, GreaterThanEqualValue(0)]
        //public double Amount { get; set; }

        ///// <summary>
        ///// Payment style
        ///// </summary>
        //[System.ComponentModel.DefaultValueAttribute(LabourUnitType.perHead)]
        //[Description("Payment style")]
        //[Required]
        //public LabourUnitType PaymentStyle { get; set; }

        ///// <summary>
        ///// Bank account to use
        ///// </summary>
        //[Description("Bank account to use")]
        //[Models.Core.Display(DisplayType = Models.Core.DisplayAttribute.DisplayTypeEnum.CLEMResourceName, CLEMResourceNameResourceGroups = new Type[] { typeof(Finance) })]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Bank account required")]
        //public string AccountName { get; set; }

        /// <summary>
        /// Validate object
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            //switch (PaymentStyle)
            //{
            //    case LabourUnitType.Fixed:
            //    case LabourUnitType.perHead:
            //        break;
            //    default:
            //        string[] memberNames = new string[] { "PaymentStyle" };
            //        results.Add(new ValidationResult("Payment style " + PaymentStyle.ToString() + " is not supported", memberNames));
            //        break;
            //}
            return results;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LabourActivityTask()
        {
            this.SetDefaults();
        }

        /// <summary>
        /// Method to determine resources required for this activity in the current month
        /// </summary>
        /// <returns>List of required resource requests</returns>
        public override List<ResourceRequest> GetResourcesNeededForActivity()
        {
            List<ResourceRequest> resourcesNeeded = null;
            return resourcesNeeded;
        }

        /// <summary>
        /// Method used to perform activity if it can occur as soon as resources are available.
        /// </summary>
        public override void DoActivity()
        {
            return;
        }

        /// <summary>
        /// Determine the labour required for this activity based on LabourRequired items in tree
        /// </summary>
        /// <param name="Requirement">Labour requirement model</param>
        /// <returns></returns>
        public override double GetDaysLabourRequired(LabourRequirement Requirement)
        {
            // get all days required as fixed only option from requirement
            switch (Requirement.UnitType)
            {
                case LabourUnitType.Fixed:
                    return Requirement.LabourPerUnit;
                default:
                    throw new Exception(String.Format("LabourUnitType {0} is not supported for {1} in {2}", Requirement.UnitType, Requirement.Name, this.Name));
            }
        }

        /// <summary>
        /// The method allows the activity to adjust resources requested based on shortfalls (e.g. labour) before they are taken from the pools
        /// </summary>
        public override void AdjustResourcesNeededForActivity()
        {
            return;
        }

        /// <summary>
        /// Method to determine resources required for initialisation of this activity
        /// </summary>
        /// <returns></returns>
        public override List<ResourceRequest> GetResourcesNeededForinitialisation()
        {
            return null;
        }

        /// <summary>
        /// Resource shortfall event handler
        /// </summary>
        public override event EventHandler ResourceShortfallOccurred;

        /// <summary>
        /// Shortfall occurred 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShortfallOccurred(EventArgs e)
        {
            if (ResourceShortfallOccurred != null)
                ResourceShortfallOccurred(this, e);
        }

        /// <summary>
        /// Resource shortfall occured event handler
        /// </summary>
        public override event EventHandler ActivityPerformed;

        /// <summary>
        /// Shortfall occurred 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnActivityPerformed(EventArgs e)
        {
            if (ActivityPerformed != null)
                ActivityPerformed(this, e);
        }

        /// <summary>
        /// Provides the description of the model settings for summary (GetFullSummary)
        /// </summary>
        /// <param name="FormatForParentControl">Use full verbose description</param>
        /// <returns></returns>
        public override string ModelSummary(bool FormatForParentControl)
        {
            string html = "";
            return html;
        }


    }
}
