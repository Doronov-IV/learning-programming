module.exports = class OperatorApplication {

    public run() {
        this.runConditional()
    }

    private runEquality() {

        this.doVerboseAction("conditional-operator-test", () => {
            let someUndefinedNumber
            let nullValue = 0
            let negativeNullValue = -0
            let notANumber = NaN
            let positiveInf = Infinity
            let negativeInf = -Infinity

            showLooseEquals("nan", "nan", NaN, NaN)
            showLooseEquals("pos inf", "true", Infinity, true)
            showLooseEquals("pos inf", "pos inf", Infinity, Infinity)
            showLooseEquals("pos inf", "neg inf", Infinity, -Infinity)
            showLooseEquals("undefined-value", "true", someUndefinedNumber, true)

            showLooseEquals("empty-array", "true", [], true)
            showLooseEquals("one-elem-array", "true", [2], true)

            showLooseEquals("some-object", "true", { a: 5 }, true)
            showLooseEquals("empty-object", "true", {}, true)

            function showLooseEquals(valueNameOne: string, valueNameTwo: string, valueOne, valueTwo): void {
                console.log("\t\t" + valueNameOne + " loose equals " + valueNameTwo + ": " + myLooseEquals(valueOne, valueTwo) + "\n")
            }

            function myLooseEquals(one, two): boolean {
                return (one == two)
            }
        })
    }


    private runConditional() {
        this.doVerboseAction("conditional-operator-test", () => {
            let undefinedValue = 3

            let messageString: string = "\tvalue is " + (undefinedValue ? undefinedValue : "[object Object]") // :p

            console.log("\t" + messageString + "\n")
        })

    }


    private doVerboseAction(actionName: string, callback) {
        console.log("\n\t[manual] action \"" + actionName + "\" has started.\n")
        callback()
        console.log("\t[manual] action \"" + actionName + "\" has been run.\n")
    }

}