using System;
using System.Collections.Generic;
using System.Text;

namespace FancyConsole.Services {
    public static class ServiceManager {
        private static List<Service> services = [];

        /// <summary>Register a new service into services</summary>
        /// <remarks>While services does support it, services should rarely have the same object type.
        /// Services typically are singletons</remarks>
        public static T Register<T>(object t, string identifier) {
            Service newDef = new(t, identifier);
            services.Add(new(t, identifier));
            return (T)t;
        }

        /// <summary>Get the first service that matches object type</summary>
        public static bool TryGetService<T>(out Service value) {
            if (services.Find(x => x.type == typeof(T)) is Service service && service != null) {
                value = service;
                return true;
            }

            value = default!;
            return false;
        }
        /// <summary>Get service specifically by ID</summary>
        public static bool TryGetService(string id, out Service value) {
            if (services.Find(x => x.identifier == id) is Service service && service != null) {
                value = service;
                return true;
            }
            value = default!;
            return false;
        }

        /// <summary>Get all services of the type </summary>
        public static bool TryGetAllServices<T>( out List<Service> value) {
            if (services.FindAll(x => x.type == typeof(T)) is List<Service> fndServices && fndServices != null) {
                value = services;
                return true;
            }
            value = default!;
            return false;
        }

        /// <summary>Directly get object of the first matching service by type</summary>
        public static bool GetObject<T>(out T value) {
            //Match by 
            if (services.Find(x => x.type == typeof(T)) is Service service) {
                value = service.GetObj<T>();
                return true;
            }
            //Nothing was found
            value = default!;
            return false;
        }

        /// <summary>Try and directly get an object by identifier</summary>
        public static bool GetObject<T>(string ID, out T value) {
            //Find the first item where type and identifier match, check if null
            if (services.Find(x => x.type == typeof(T) && x.identifier == ID) is Service service && service != null) {
                value = service.GetObj<T>();
                return true;
            }

            //Nothing found
            value = default!;
            return false;
        }

        /// <summary>Try and get all objects that have the same service type</summary>
        public static bool GetAllObjects<T>(out List<T> value) {
            //Get list of all Services which are of type T
            if (services.FindAll(x => x.type == typeof(T)) is List<Service> fndObjects && fndObjects != null) {
                value = [];
                //Iterate through each found service, and add to list
                foreach (Service service in fndObjects) {
                    value.Add(service.GetObj<T>());
                }
                return true;
            }

            //Nothing found
            value = default!;
            return false;
        }
    }
}
