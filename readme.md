# JK Custom wind switch mod

The mod allows you to customise the 2 ways wind block behaviour.

In the level_settings.xml file, in the Tags scope, just add the following line (or add a Worldsmith tag):
```xml
<Tags>
    <string>CustomWindSwitch=(1,2,2),(2,12.5,5,right)</string>
</Tags>
```
On this example, for screen 1 you will get 2 seconds left, 2 seconds right. And for screen 2, you will get 12.5 seconds wind right, and 5 seconds of no wind.

It is a list (delimited by ',') of tuple.

Parameters of the tuple:
- (int) screen index: the screen index where the wind block is (first screen is 1).
- (float) left time: the time of the wind block blowing left.
- (float) right time: the time of the wind block blowing right.
- (optional string) no wind direction: the direction of the wind block when there is no wind. Can be _left_ or _right_.

You need to add the 2 ways wind blocks inside the level.xnb file, on the corresponding screens.
