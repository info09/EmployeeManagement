namespace EmployeeManagementSystem.Client.ApplicationStates
{
    public class AllState
    {
        public Action? Action { get; set; }

        #region GeneralDepartment
        public bool ShowGeneralDepartment { get; set; }
        public void GeneralDepartmentClicked()
        {
            ResetAllDepartments();
            ShowGeneralDepartment = true;
            Action?.Invoke();
        }
        #endregion


        #region Department
        public bool ShowDepartment { get; set; }
        public void DepartmentClicked()
        {
            ResetAllDepartments();
            ShowDepartment = true;
            Action?.Invoke();
        }
        #endregion


        #region Branch
        public bool ShowBranch { get; set; }
        public void BranchClicked()
        {
            ResetAllDepartments();
            ShowBranch = true;
            Action?.Invoke();
        }
        #endregion


        #region User
        public bool ShowUser { get; set; }
        public void UserClicked()
        {
            ResetAllDepartments();
            ShowUser = true;
            Action?.Invoke();
        }
        #endregion


        #region Employee
        public bool ShowEmployee { get; set; }
        public void EmployeeClicked()
        {
            ResetAllDepartments();
            ShowEmployee = true;
            Action?.Invoke();
        }
        #endregion

        private void ResetAllDepartments()
        {
            ShowGeneralDepartment = false;
            ShowDepartment = false;
            ShowBranch = false;
            ShowUser = false;
            ShowEmployee = false;
        }
    }
}
