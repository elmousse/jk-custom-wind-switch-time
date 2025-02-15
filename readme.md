# JK Custom wind switch mod

The mod allows you to change the period of the 2 ways wind block.

In the level_settings.xml file, in the Tags scope, just add the following line (or add a worldsmith tags):
```xml
<Tags>
    <string>CustomWindSwitch=(1,2;2,12.5)</string>
</Tags>
```
For example, for screen 1 you will get 2 seconds period, and for screen 2 you will get 12.5 seconds period.

It is a list (delimited by ';') of pairs (delimited by ','). The first element of the pair is the screen number (int), the second element is the period of the 2 ways wind(float).

You need to add the 2 ways wind blocks inside the level.xnb file, on the corresponding screens.
