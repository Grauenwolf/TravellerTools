using Grauenwolf.TravellerTools.Equipment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;

namespace EquipmentTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string DataPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var eb = new EquipmentBuilder(DataPath);

                var store = eb.AvailabilityTable(new StoreOptions() { LawLevel = 0, Starport = "A", TechLevel = 20 });

                var results = new List<string>();
                foreach (var section in store.Sections.OrderBy(s => s.Name))
                {
                    results.Add("");
                    results.Add("Section " + section.Name);
                    foreach (var item in section.Items.OrderBy(s => s.Name).ThenBy(s => s.TechLevel))
                    {
                        results.Add($"    {item}");
                    }

                    foreach (var subsection in section.Subsections.OrderBy(s => s.Name))
                    {
                        results.Add("");
                        results.Add("    *** " + subsection.Name + " ***");
                        foreach (var item in subsection.Items.OrderBy(s => s.Name).ThenBy(s => s.TechLevel))
                        {
                            results.Add($"        {item}");
                        }
                    }

                }

                Store.ItemsSource = results;

            }

            catch (Exception ex)
            {
                Store.ItemsSource = new List<string>(ex.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var file = new FileInfo(Path.Combine(DataPath, "Equipment.xml"));
                var fileOut = new FileInfo(Path.Combine(DataPath, "Equipment-Sorted.xml"));
                var converter = new XmlSerializer(typeof(Catalog));

                using (var stream = file.OpenRead())
                using (var stream2 = fileOut.OpenWrite())
                {
                    var book = (Catalog)converter.Deserialize(stream);

                    book.Section = book.Section.OrderBy(s => s.Name).ToArray();
                    foreach (var section in book.Section)
                    {
                        if (section.Item != null)
                            section.Item = section.Item.OrderBy(i => i.Name).ThenBy(i => i.TL).ToArray();
                        if (section.Subsection != null)
                            section.Subsection = section.Subsection.OrderBy(i => i.Name).ToArray();

                        if (section.Subsection != null)
                            foreach (var subsection in section.Subsection)
                            {
                                if (subsection.Item != null)
                                    subsection.Item = subsection.Item.OrderBy(i => i.Name).ThenBy(i => i.TL).ToArray();
                            }

                    }

                    converter.Serialize(stream2, book);
                }
                MessageBox.Show("Sorted file created as Equipment-Sorted.xml");
            }
            catch (Exception ex)
            {
                Store.ItemsSource = new List<string>(ex.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            }
        }
    }
}
