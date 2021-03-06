namespace Alg1.Practica.Practicum5
{
    public class XmlValidator
    {

        public bool Validate(string xml)
        {
            if (!xml.Contains("<"))
                return true;

            Stack stack = new Stack();

            int start;
            int end;
            int counter = 0;
            string newString;

            while (xml.Length > 0) 
            {
                if (xml.Contains("<") && xml.Contains(">"))
                {
                    start = xml.IndexOf('<');
                    end = xml.IndexOf('>') - start;
                    newString = xml.Substring(start, end + 1);

                    if (newString.Contains("/"))
                    {
                        if (counter == 0)
                            return false;
                        
                        if (stack.Peek().Contains(newString.Substring(2, newString.Length-2)))
                        {
                            stack.Pop();
                            counter--;
                            xml = xml.Substring(xml.IndexOf('>') + 1);
                        }
                        else
                            return false;
                    }
                    else
                    {
                        stack.Push(newString);
                        xml = xml.Substring(xml.IndexOf('>') + 1);
                        counter++;
                    }
                }
                else
                    return false;
            }

            if(counter != 0)
                return false;

            return true;
        }
    }
}


