using System.Collections.Generic;
using SSL_Core.model.roles;

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
        /// Permet de set un rôle existant
        /// </summary>
        public void SetRole(string roleName, RolePickerChoice choice)
        {
            choices[roleName] = choice;
        }
        
        /// <summary>
        /// Permet de get un choice
        /// </summary>
        public RolePickerChoice GetRole(string roleName)
        {
            return choices[roleName];
        }
    }
}