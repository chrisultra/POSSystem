using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace POSSystem
{
    public class CatInfo
    {

        private static CatInfo categories;

        private Dictionary<string, Category> catDictionary;
        private BinaryFormatter formatter;

        private const string DATAFILE = "categories.dat";

        public static CatInfo Instance()
        {
            if (categories == null)
            {
                categories = new CatInfo();
            } // end if

            return categories;
        } // end public static Categories Instance()

        private CatInfo()
        {
            // Create a Dictionary to store friends at runtime
            this.catDictionary = new Dictionary<string, Category>();
            this.formatter = new BinaryFormatter();
        } // end private Categories()

        public void AddCategory(string name)
        {
            // If we already had added a friend with this name
            if (this.catDictionary.ContainsKey(name))
            {
                Console.WriteLine("You had already added " + name + " before.");
            }
            // Else if we do not have this friend details 
            // in our dictionary
            else
            {
                // Add him in the dictionary
                //this.catDictionary.Add(name, new Category(name));
                Console.WriteLine("Friend added successfully.");
            } // end if
        } // end public bool AddFriend(string name, string email)

        public void RemoveCategory(string name)
        {
            // If we do not have a friend with this name
            if (!this.catDictionary.ContainsKey(name))
            {
                Console.WriteLine(name + " had not been added before.");
            }
            // Else if we have a friend with this name
            else
            {
                if (this.catDictionary.Remove(name))
                {
                    Console.WriteLine(name + " had been removed successfully.");
                }
                else
                {
                    Console.WriteLine("Unable to remove " + name);
                } // end if
            } // end if
        } // end public bool RemoveFriend(string name)

        public void Save()
        {
            // Gain code access to the file that we are going
            // to write to
            try
            {
                // Create a FileStream that will write data to file.
                FileStream writerFileStream =
                    new FileStream(DATAFILE, FileMode.Create, FileAccess.Write);
                // Save our dictionary of friends to file
                this.formatter.Serialize(writerFileStream, this.catDictionary);

                // Close the writerFileStream when we are done.
                writerFileStream.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to save our friends' information");
            } // end try-catch
        } // end public bool Load()

        public void Load()
        {

            // Check if we had previously Save information of our friends
            // previously
            if (File.Exists(DATAFILE))
            {

                try
                {
                    // Create a FileStream will gain read access to the 
                    // data file.
                    FileStream readerFileStream = new FileStream(DATAFILE,
                        FileMode.Open, FileAccess.Read);
                    // Reconstruct information of our friends from file.
                    this.catDictionary = (Dictionary<String, Category>)
                        this.formatter.Deserialize(readerFileStream);
                    // Close the readerFileStream when we are done
                    readerFileStream.Close();

                }
                catch (Exception)
                {
                    Console.WriteLine("There seems to be a file that contains " +
                        "friends information but somehow there is a problem " +
                        "with reading it.");
                } // end try-catch

            } // end if

        } // end public bool Load()

        public void Print()
        {
            // If we have saved information about friends
            if (this.catDictionary.Count > 0)
            {
                //Console.WriteLine("Name, Email");
                foreach (Category category in this.catDictionary.Values)
                {
                    Console.WriteLine(category.name);
                } // end foreach
            }
            else
            {
                Console.WriteLine("There are no saved information about your friends");
            } // end if
        } // end public void Print()

    } // end public class CatInfo
}
