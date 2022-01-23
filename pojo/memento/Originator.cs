using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using svg_memento.exception;
using svg_memento.pojo.face;
using svg_memento.utils;

namespace svg_memento.pojo.memento
{
    /// <summary>
    /// The Originator contains all the methods that may change the State,
    /// provides the methods that interact with the State for the Caretaker
    /// </summary>
    public class Originator
    {
        /// <summary>
        /// The originator's state, references the face class that contains all the organs detail
        /// </summary>
        private Face State;

        public Originator(Face state)
        {
            this.State = state;
        }

        /// <summary>
        /// Save the current state to a new memento for the Caretaker to backup,
        /// as the Caretaker is not permitted to alter memento
        /// </summary>
        /// <returns></returns>
        public IMemento Save()
        {
            return new ConcreteMemento(this.State);
        }

        /// <summary>
        /// Restore current state from a memento object.
        /// In order to avoid the passing by reference issue, the state object should be dept clone
        /// </summary>
        /// <param name="memento"></param>
        public void Restore(IMemento memento)
        {
            this.State = ObjectUtils.DeepClone(memento.GetState());
            Console.WriteLine(GenSvgCode());
        }
        
        /// <summary>
        /// Rest current state to a new reference
        /// </summary>
        public void Restore()
        {
            this.State = new Face();
            Console.WriteLine(GenSvgCode());
        }

        /// <summary>
        /// Print current svg
        /// </summary>
        public void Draw()
        {
            Console.WriteLine(GenSvgCode());
        }

        /// <summary>
        /// Generate the svg code
        /// </summary>
        /// <returns></returns>
        public string GenSvgCode()
        {
            StringBuilder sb = new();

            // header
            sb.Append(@"<svg viewBox=""0 0 72 72"" xmlns=""http://www.w3.org/2000/svg"" >");
            sb.Append(Environment.NewLine);
            
            // content
            sb.Append("<!-- Face -->");
            sb.Append(Environment.NewLine);
            sb.Append(@"<circle cx=""36"" cy=""36"" r=""23"" fill=""#FCEA2B"" stroke=""#000000"" stroke-miterlimit=""10"" stroke-width=""2""/>");
            sb.Append(Environment.NewLine);
            
            sb.Append("<!-- Left brow -->");
            sb.Append(Environment.NewLine);
            sb.Append(this.State.LeftBrow.Style.GenSvgCode());
            sb.Append(Environment.NewLine);
            
            sb.Append("<!-- Right brow -->");
            sb.Append(Environment.NewLine);
            sb.Append(this.State.RightBrow.Style.GenSvgCode());
            sb.Append(Environment.NewLine);
            
            sb.Append("<!-- Left eye -->");
            sb.Append(Environment.NewLine);
            sb.Append(this.State.LeftEye.Style.GenSvgCode());
            sb.Append(Environment.NewLine);
            
            sb.Append("<!-- Right eye -->");
            sb.Append(Environment.NewLine);
            sb.Append(this.State.RightEye.Style.GenSvgCode());
            sb.Append(Environment.NewLine);
            
            sb.Append("<!-- Mouth -->");
            sb.Append(Environment.NewLine);
            sb.Append(this.State.Mouth.Style.GenSvgCode());
            sb.Append(Environment.NewLine);
            
            // footer
            sb.Append("</svg>");

            return sb.ToString();
        }

        /// <summary>
        /// Show an organ by command
        /// </summary>
        /// <param name="cmd"></param>
        public void Show(string cmd)
        {
            ToggleShow(cmd, true);
        }
        
        /// <summary>
        /// Hide an organ by command
        /// </summary>
        /// <param name="cmd"></param>
        public void Hide(string cmd)
        {
            ToggleShow(cmd, false);
        }

        /// <summary>
        /// Get the command arguments
        /// </summary>
        /// <param name="cmdStr"></param>
        /// <param name="commandName"></param>
        /// <param name="expectedCount"></param>
        /// <returns></returns>
        /// <exception cref="MyException"></exception>
        /// Throw the exception if the arguments count does not equal to expected count
        private List<string> GetArgs(string cmdStr, string commandName, int expectedCount)
        {
            List<string> argList = new List<string>();
            var args = cmdStr.Split(" ");
            foreach (var arg in args)
            {
                var trimArg = arg.Trim();
                if (trimArg.Length > 0 && trimArg != commandName)
                {
                    argList.Add(trimArg);
                }
            }

            if (argList.Count != expectedCount)
            {
                throw new MyException($"Invalid '{commandName}' command, use 'help' for help.");
            }

            return argList;
        }

        /// <summary>
        /// Toggle the isShown property in Style class
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="isShow"></param>
        private void ToggleShow(string cmd, bool isShow)
        {
            string commandName = "show";
            string commandDesc = "added to";
            if (!isShow)
            {
                commandName = "hide";
                commandDesc = "hidden from";
            }

            List<string> args = GetArgs(cmd, commandName, 1);
            Organ operateOrgan = GetOrganByStr(args[0], commandName);
            operateOrgan.Style.IsShown = isShow;
            Console.WriteLine($"{operateOrgan.OrganName} ({operateOrgan.Style.StyleName}) {commandDesc} emoticon.");
        }

        /// <summary>
        /// Get the corresponding organ by a string
        /// </summary>
        /// <param name="organStr"></param>
        /// <param name="commandName"></param>
        /// <returns></returns>
        /// <exception cref="MyException"></exception>
        /// Throw the exception if there are not matched string for organStr
        private Organ GetOrganByStr(string organStr, string commandName)
        {
            Organ operateOrgan;
            switch (organStr)
            {
                case "left-eye":
                    operateOrgan = this.State.LeftEye;
                    break;
                case "right-eye":
                    operateOrgan = this.State.RightEye;
                    break;
                case "left-brow":
                    operateOrgan = this.State.LeftBrow;
                    break;
                case "right-brow":
                    operateOrgan = this.State.RightBrow;
                    break;
                case "mouth":
                    operateOrgan = this.State.Mouth;
                    break;
                default:
                    throw new MyException($"Invalid '{commandName}' command, use 'help' for help.");
            }

            return operateOrgan;
        }

        /// <summary>
        /// Move an organ by command
        /// </summary>
        /// <param name="cmd"></param>
        /// <exception cref="MyException"></exception>
        public void Move(string cmd)
        {
            string commandName = "move";
            try
            {
                List<string> args = GetArgs(cmd, commandName, 3);
                string direction = args[1];
                int moveLength = int.Parse(args[2]);
                Organ operateOrgan = GetOrganByStr(args[0], commandName);
                MoveByDirection(direction, moveLength, operateOrgan);
                Console.WriteLine(
                    $"{operateOrgan.OrganName} ({operateOrgan.Style.StyleName}) moved {direction} {moveLength}px.");
            }
            catch (Exception e)
            {
                if (e is MyException)
                {
                    throw;
                }

                throw new MyException($"Invalid '{commandName}' command, use 'help' for help.");
            }
        }

        /// <summary>
        /// Move the specific organ
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="value"></param>
        /// <param name="organ"></param>
        /// <exception cref="MyException"></exception>
        private void MoveByDirection(string direction, int value, Organ organ)
        {
            switch (direction)
            {
                case "up":
                    organ.MoveUp(value);
                    break;
                case "down":
                    organ.MoveDown(value);
                    break;
                case "left":
                    organ.MoveLeft(value);
                    break;
                case "right":
                    organ.MoveRight(value);
                    break;
                default:
                    throw new MyException(@$"Invalid direction: {direction}, use 'help' for help.");
            }
        }

        /// <summary>
        /// Rest an organ to the default style
        /// </summary>
        /// <param name="cmd"></param>
        public void Reset(string cmd)
        {
            string commandName = "reset";
            List<string> args = GetArgs(cmd, commandName, 1);
            Organ operateOrgan = GetOrganByStr(args[0], commandName);
            operateOrgan.Reset();
            Console.WriteLine($"Set {operateOrgan.OrganName} style to default style: {operateOrgan.Style.StyleName}.");
        }

        /// <summary>
        /// Set the organ's style
        /// </summary>
        /// <param name="cmd"></param>
        /// <exception cref="MyException"></exception>
        public void SetStyle(string cmd)
        {
            string commandName = "style";
            List<string> args = GetArgs(cmd, commandName, 2);
            Organ operateOrgan = GetOrganByStr(args[0], commandName);
            string styleName = args[1];
            switch (styleName)
            {
                case "a":
                    operateOrgan.SetStyleA();
                    break;
                case "b":
                    operateOrgan.SetStyleB();
                    break;
                default:
                    throw new MyException(@$"Invalid style: {styleName}, currently supported styles: a, b.");
            }

            Console.WriteLine($"Set {operateOrgan.OrganName} style to style: {operateOrgan.Style.StyleName}.");
        }

        /// <summary>
        /// Save to a svg file
        /// </summary>
        /// <param name="cmd"></param>
        /// <exception cref="MyException"></exception>
        public void SaveSvgFile(string cmd)
        {
            string commandName = "save";
            List<string> args = GetArgs(cmd, commandName, 1);
            string fileName = args[0];
            if (fileName.Length < 5 || !fileName.EndsWith(".svg"))
            {
                throw new MyException($"Invalid file name: {fileName}, file name should look like: fileName.svg");
            }
            
            string text = GenSvgCode();
            File.WriteAllText(fileName, text);
            Console.WriteLine($"File: {fileName} has been saved!");
        }
    }
}