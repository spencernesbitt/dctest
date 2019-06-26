# DesignCrowd Test Solution - Spencer Nesbitt
I have implemented the test in dot net core 2.1, assuming you have this or a later version installed (https://dotnet.microsoft.com/download/dotnet-core/2.1), run the following from the folder that you have cloned the repo into:

`dotnet test`

## Implementation Notes
The implementation of the spec is contained in a class library (**designcrowd.spencer.logic**) and the functionality is excercised from a XUnit based unit test project (**designcrowd.spencer.tests**).

I have added XML based inline documentation to the main classes and added data driven tests where appropriate. Within the class library I have separated each class, interface etc. into its own file as I find this approach generally easier to navigate and code review. This is a personal preference but I understand that many developers prefer to group related classes into a single file.

I incorporated the rule based Public Holiday functionality by overloading the **BusinessDaysBetweenTwoDates** method to take a list of public holiday rules as specified. I have also retained the original signiture that takes a list of date values as the spec says to extend the class rather than replace the method. I also feel that being able to take either list makes sense from a user's point of view. 

The starter code provided by the specification uses non-static members for the **BusinessDayCounter** class and there is no requirement to change these methods to static so I have left them as is. Given the end result does not require any instance members, I would tend to make these methods static in a 'real' situation.

