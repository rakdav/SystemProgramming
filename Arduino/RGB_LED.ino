String option;
void setup()
{
  pinMode(13, OUTPUT);
   Serial.begin(9600);
}
 
void loop()
{
  if (Serial.available() > 0) 
  {
      option = Serial.readStringUntil('\n');
      option.trim();
      if (option == "On") 
      {
        Serial.println("Свет включен");
        digitalWrite(13, HIGH);
      }
      else
      {
        Serial.println("Свет выключен");
        digitalWrite(13, LOW);
      }
  }
}