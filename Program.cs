using System;
using svg_memento.exception;
using svg_memento.pojo.face;
using svg_memento.pojo.memento;

namespace svg_memento
{
    class Program
    {
        /// <summary>
        /// Student Number: 21250028.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // The client code
            Originator originator = new (new Face());
            Caretaker caretaker = new (originator);
            
            Console.WriteLine();
            Console.WriteLine("Emoticon created - use commands to show, hide, move or style features to the emoticon.");
            ShowHelpMsg();
            bool showTheMenu = true;
            while (showTheMenu)
            {
                try
                {
                    showTheMenu = MainMenu(originator, caretaker);
                }
                catch (Exception ex)
                {
                    if (ex is MyException)
                    {
                        Console.WriteLine(ex.Message);
                    } else
                    {
                        Console.WriteLine("Oooops! System error: {0}", ex.Message);
                    }
                }
            }
        }
        
        //
        // This section contains the application menu
        //
        private static bool MainMenu(Originator originator, Caretaker caretaker)
        {
            string cmd = Console.ReadLine();
            cmd = cmd.ToLower().Trim();
            if (cmd == "quit")
            {
                Console.WriteLine("Goodbye!");
                return false;
            }
            if (cmd.StartsWith("show "))
            {
                originator.Show(cmd);
                caretaker.Backup();
            }
            else if (cmd.StartsWith("hide "))
            {
                originator.Hide(cmd);
                caretaker.Backup();
            }
            else if (cmd.StartsWith("move "))
            {
                originator.Move(cmd);
                caretaker.Backup();
            }
            else if (cmd.StartsWith("reset "))
            {
                originator.Reset(cmd);
                caretaker.Backup();
            }
            else if (cmd.StartsWith("style "))
            {
                originator.SetStyle(cmd);
                caretaker.Backup();
            }
            else if (cmd.StartsWith("save "))
            {
                originator.SaveSvgFile(cmd);
            }
            else if (cmd == "draw")
            {
                originator.Draw();
            }
            else if (cmd == "undo")
            {
                var success = caretaker.Undo();
                if (!success)
                {
                    Console.WriteLine("Nothing to undo...");
                    Console.WriteLine();
                    return true;
                }
                Console.WriteLine("Undo success, emotion updated:");
                originator.Draw();
            }
            else if (cmd == "redo")
            {
                var success = caretaker.Redo();
                if (!success)
                {
                    Console.WriteLine("Nothing to redo...");
                    Console.WriteLine();
                    return true;
                }
                Console.WriteLine("Redo success, emotion updated:");
                originator.Draw();
            }
            else if (cmd == "help")
            {
                ShowHelpMsg();
            }
            else
            {
                Console.WriteLine("Invalid directive, use 'help' for help");
            }
            Console.WriteLine();
            return true;
        }
        
        /// <summary>
        /// Show help message
        /// </summary>
        private static void ShowHelpMsg()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("          show   { left-eye | right-eye | left-brow | right-brow | mouth }");
            Console.WriteLine("          hide   { left-eye | right-eye | left-brow | right-brow | mouth }");
            Console.WriteLine("          move   { left-eye | right-eye | left-brow | right-brow | mouth } {up | down | left | right } value");
            Console.WriteLine("          reset  { left-eye | right-eye | left-brow | right-brow | mouth }");
            Console.WriteLine("          style  { left-eye | right-eye | left-brow | right-brow | mouth } {a | b}");
            Console.WriteLine("          save   { <file> }");
            Console.WriteLine("          draw");
            Console.WriteLine("          undo");
            Console.WriteLine("          redo");
            Console.WriteLine("          help");
            Console.WriteLine("          quit");
        }
    }
}