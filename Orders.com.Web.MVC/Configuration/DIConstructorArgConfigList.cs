using System.Collections.Generic;
using System.Configuration;

namespace Orders.com.Web.MVC.Configuration
{
    [ConfigurationCollection(typeof(DIConstructorArgConfig))]
    public class DIConstructorArgConfigList : ConfigurationElementCollection, IEnumerable<DIConstructorArgConfig>
    {
        /// <summary>
        /// Gets the <see cref="DIConstructorArgConfig"/> configuration element at the specified index.
        /// </summary>
        /// <returns>The specified <see cref="DIConstructorArgConfig"/> configuration element.</returns>
        /// <param name="index">The index to retrieve.</param>
        public DIConstructorArgConfig this[int index]
        {
            get { return (DIConstructorArgConfig)BaseGet(index); }
        }

        /// <summary>
        /// Adds the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public void Add(DIConstructorArgConfig entry)
        {
            this.BaseAdd(entry);
        }

        /// <summary>
        /// Creates a new <see cref="DIConstructorArgConfig"/>.
        /// </summary>
        /// <returns>
        /// A new <see cref="DIConstructorArgConfig"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new DIConstructorArgConfig();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <param name="element">The <see cref="FileDestinationConfig"/> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.String"/> that acts as the key for the specified <see cref="FileDestinationConfig"/>.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DIConstructorArgConfig)element).ArgumentName;
        }

        public IEnumerable<DIConstructorArgConfig> GetAll()
        {
            for (int i = 0; i < this.Count; i++)
                yield return this[i];
        }

        public new IEnumerator<DIConstructorArgConfig> GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }
    }
}
