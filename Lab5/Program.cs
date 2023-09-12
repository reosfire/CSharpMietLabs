using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Foundation;
using Lab5.Models;
using Lab5.Models.Students;

namespace Lab5
{
    internal class Program : LabBase<Mocker>
    {
        // Summary:
        // Serialization
        // Rewrite deepCopy 
        private static void Main() => new Program().Run();
        
        public static void MoveData<T1, T2>(T1 source, ref T2 destination)
        {
            FieldInfo[] sourceFields = source!.GetType().GetFields();
            FieldInfo[] destinationFields = destination!.GetType().GetFields();
            
            foreach (FieldInfo sourceField in sourceFields)
            {
                FieldInfo? destField = destinationFields.FirstOrDefault(it => it.Name == sourceField.Name);
                if (destField == null) continue;
                try
                {
                    destField.SetValue(destination, sourceField.GetValue(source));
                }
                catch
                {
                    continue;
                }
            }
            
            PropertyInfo[] sourceProps = source!.GetType().GetProperties();
            PropertyInfo[] destinationProps = destination!.GetType().GetProperties();
            
            foreach (PropertyInfo sourceProp in sourceProps)
            {
                PropertyInfo? destProp = destinationProps.FirstOrDefault(it => it.Name == sourceProp.Name);
                if (destProp == null) continue;
                try
                {
                    destProp.SetValue(destination, sourceProp.GetValue(source));
                }
                catch
                {
                    continue;
                }
            }
        }
        private static bool Save<T>(string fileName, T obj)
        {
            IFormatter formatter = new BinaryFormatter();

            FileStream? fileStream = null;
            try
            {
                fileStream = File.OpenWrite(fileName);
                formatter.Serialize(fileStream, obj);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                fileStream?.Dispose();
            }
        }
        private static bool Load<T>(string fileName, ref T obj)
        {
            IFormatter formatter = new BinaryFormatter();
            
            FileStream? fileStream = null;
            try
            {
                fileStream = File.OpenRead(fileName);
                T deserialized = (T)formatter.Deserialize(fileStream);
                
                MoveData(deserialized, ref obj);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                fileStream?.Dispose();
            }
        }

        private void Run()
        {
            RunCommented("1. DeepCopy", () =>
            {
                Student student = Mocker.MockStudent();
                Console.WriteLine(student.ToStr("Student"));
                Student copy = student.DeepCopy();
                Console.WriteLine(copy.ToStr("ItsDeepCopy"));
            });
            
            RunCommented("2. SavingLoading", () =>
            {
                string fileName;
                Student loadedStudent = new();
                while (true)
                {
                    Console.Write("Enter file name: ");
                    try
                    {
                        fileName = Console.ReadLine()!;
                        if (!File.Exists(fileName))
                        {
                            Console.WriteLine("New file created!");
                            File.Create(fileName).Dispose();
                        }
                        else
                        {
                            Console.WriteLine(loadedStudent.Load(fileName)
                                ? "Successfully loaded"
                                : "Error while loading student from file!");
                        }

                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter correct file name");
                    }
                }

                
                Console.WriteLine(loadedStudent.ToStr("Current student"));


                loadedStudent.AddFromConsole();
                Console.WriteLine(loadedStudent.Save(fileName)
                    ? "Student successfully saved to file"
                    : "Error while saving student");
                Console.WriteLine(loadedStudent.ToStr("Current student"));


                Console.WriteLine(Load(fileName, ref loadedStudent)
                    ? "Successfully loaded"
                    : "Error while loading student from file!");
                loadedStudent.AddFromConsole();
                Console.WriteLine(Save(fileName, loadedStudent)
                    ? "Student successfully saved to file"
                    : "Error while saving student");
                
                
                Console.WriteLine(loadedStudent.ToStr("Current student"));
            });
        }
    }
}