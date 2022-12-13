﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenID.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ClientServiceResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ClientServiceResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Skoruba.IdentityServer4.Admin.BusinessLogic.Resources.ClientServiceResource", typeof(ClientServiceResource).Assembly);
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
        ///   Looks up a localized string similar to Client claim with id {0} doesn&apos;t exist.
        /// </summary>
        internal static string ClientClaimDoesNotExist {
            get {
                return ResourceManager.GetString("ClientClaimDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Client with id {0} doesn&apos;t exist.
        /// </summary>
        internal static string ClientDoesNotExist {
            get {
                return ResourceManager.GetString("ClientDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Client already exists.
        /// </summary>
        internal static string ClientExistsKey {
            get {
                return ResourceManager.GetString("ClientExistsKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Client Id ({0}) already exists.
        /// </summary>
        internal static string ClientExistsValue {
            get {
                return ResourceManager.GetString("ClientExistsValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Client property with id {0} doesn&apos;t exist.
        /// </summary>
        internal static string ClientPropertyDoesNotExist {
            get {
                return ResourceManager.GetString("ClientPropertyDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Client secret with id {0} doesn&apos;t exist.
        /// </summary>
        internal static string ClientSecretDoesNotExist {
            get {
                return ResourceManager.GetString("ClientSecretDoesNotExist", resourceCulture);
            }
        }
    }
}
