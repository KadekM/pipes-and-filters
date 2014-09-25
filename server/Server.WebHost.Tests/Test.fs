module Test

open NUnit.Framework
open Server.WebHost


[<TestFixture>]
type TestClass() = 

    [<Test>]
    member this.When2IsAddedTo2Expect4() = 
        let gl = new Global()
        ()