module.exports = class ExpressionStatementsApplication {
    public run(): void {
        this.runTimes();
    }


    public runTimes() {
        this.doVerboseAction("times-test", () => {
            // unfortunately, this example does not work
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
            let a = [1] // ;
            [1,2,3].push(4)
        })
    }


    private doVerboseAction(actionName: string, callback) {
        console.log("\n\t[manual] action \"" + actionName + "\" has started.\n")
        callback()
        console.log("\t[manual] action \"" + actionName + "\" has been run.\n")
    }
}