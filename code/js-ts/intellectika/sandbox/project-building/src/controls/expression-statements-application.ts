let commonApp = require('./common-application')

module.exports = class ExpressionStatementsApplication extends commonApp {
    public run(): void {
        this.runLoops();
    }


    public runTimes() {
        this.doVerboseAction("times-test", () => {
            // unfortunately, this example does not work for me rn
            // 
            // let a = 5;
            // (a).times(function(n) {
            //     console.log("hello")
            // })
            //

            // an expression written below is being undersigned red in my vscode.
            // the expression itself cannot handle w/o a semicolon, my node.js is denying me compilation.
            // luckily, the ide/idle/text-editor can typically give a hint on that.
            // although in this case it might be due to the addon (extension) I installed previously.
            let a = [1]; // ;
            [1,2,3].push(4)
        })
    }


    public runLoops() {
        this.doVerboseAction("loops-test", () => {
            let obj = {
                a: 5,
                b: "some string",
                c: {
                    a1: 2,
                    b1: "some other string"
                }
            }


            let show = function(obj: Object): void {
                for(let unit in obj) {
                    console.log(unit)
                }
            }


            for (let item in obj) {
                if (typeof item == "object") {
                    show(item)
                }

                else console.log(item + ": " + obj[item] + "\n")
            }

            
        })
    }

}