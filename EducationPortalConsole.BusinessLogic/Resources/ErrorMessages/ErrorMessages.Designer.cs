﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EducationPortalConsole.BusinessLogic.Resources.ErrorMessages {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EducationPortalConsole.BusinessLogic.Resources.ErrorMessages.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email not found.
        /// </summary>
        internal static string EmailNotFound {
            get {
                return ResourceManager.GetString("EmailNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email is already taken.
        /// </summary>
        internal static string EmailTaken {
            get {
                return ResourceManager.GetString("EmailTaken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Skill id is empty.
        /// </summary>
        internal static string SkillGuidEmpty {
            get {
                return ResourceManager.GetString("SkillGuidEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SkillIsNull.
        /// </summary>
        internal static string SkillIsNull {
            get {
                return ResourceManager.GetString("SkillIsNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Skill validation error.
        /// </summary>
        internal static string SkillValidationError {
            get {
                return ResourceManager.GetString("SkillValidationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User is null.
        /// </summary>
        internal static string UserIsNull {
            get {
                return ResourceManager.GetString("UserIsNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Username not found.
        /// </summary>
        internal static string UsernameNotFound {
            get {
                return ResourceManager.GetString("UsernameNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Username is already exists.
        /// </summary>
        internal static string UsernameTaken {
            get {
                return ResourceManager.GetString("UsernameTaken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User validation error.
        /// </summary>
        internal static string UserValidationError {
            get {
                return ResourceManager.GetString("UserValidationError", resourceCulture);
            }
        }
    }
}
