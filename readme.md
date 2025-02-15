# JK Custom wind switch time mod

The mod allows you to change the period of the switch wind block.

In the level_settings.xml file, in the Tags scope, just add the following line:
```xml
<Tags>
    <string>CustomWindSwitchTime=(1,2;2,12.5)</string>
</Tags>
```
For example, for screen 1 you will get 2 seconds period, and for screen 2 you will get 12.5 seconds period.

You need to add the 2 ways wind blocks inside the level.xnb file, on the corresponding screens.

It is a list (delimited by ';') of pairs (delimited by ','). The first element of the pair is the screen number, the second element is the period of the 2 ways wind.