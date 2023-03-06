module.exports = class CommonApplication {
 
    public run() {
        
    }

    private doVerboseAction(actionName: string, callback) {
        console.log("\n\t[manual] action \"" + actionName + "\" has started.\n")
        callback()
        console.log("\t[manual] action \"" + actionName + "\" has been run.\n")
    }

}