using System.Collections.Generic;

namespace SSL_Core.services
{
    public class RolePicker
    {
        private Dictionary<string, RolePickerChoice> choices;

        public RolePicker()
        {
            choices = new Dictionary<string, RolePickerChoice>();
        }
        
        public RolePicker(IEnumerable<string> roles) : this()
        {
            foreach (string role in roles)
            {
                choices.Add(role, RolePickerChoice.Never);
            }
        }
        
        public RolePicker(Dictionary<string, RolePickerChoice> choices)
        {
            this.choices = choices;
        }

        /// <summary>
        /// Modifies an existing role
        /// </summary>
        public void SetRole(string roleName, RolePickerChoice choice)
        {
            choices[roleName] = choice;
        }
        
        /// <summary>
        /// Returns a role choice
        /// </summary>
        public RolePickerChoice GetRole(string roleName)
        {
            return choices[roleName];
        }
    }
}