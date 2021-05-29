"use strict";

const json = require("./config.json")

class main {
    constructor() {
        Logger.info(`Loading: spaceman-hardcore`);
        HttpRouter.onStaticRoute["/mod/spaceman-harcore/config"] = {"spaceman-hardcore": this.getConfig.bind(this)};
    };

    getConfig(url, info, sessionID) {
        return HttpResponse.getBody(json);

    }
}

module.exports.main = new main;