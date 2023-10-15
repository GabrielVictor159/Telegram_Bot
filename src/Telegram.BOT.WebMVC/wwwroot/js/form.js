class FormValidator {
    formRoot
    allInputObjects
    numericInputObjects
    sizedInputObjects
    inputObjectsWithWordCount
    constructor(form) {
        this.formRoot = form
        this.allInputObjects = this.getValidableInputs()
        this.numericInputObjects = this.getValidableInputs().filter(element => element.group.classList.contains("numeric"))
        this.sizedInputObjects = this.getValidableInputs().filter(element => element.group.classList.contains("has-min-size"))
        this.inputObjectsWithWordCount = this.getValidableInputs().filter(element => element.group.classList.contains("has-word-count"))
    }

    hasEmpty() {
        this.allInputObjects.forEach((obj) => {
            let input = obj.group.querySelector("input, textarea, select")
            if (input.value == "") {
                this.invalidate(obj, "Campo deve ser preenchido")
            }
        })
    }

    hasInvalidNumbers() {
        if (this.numericInputObjects.length > 0) {
            this.numericInputObjects.forEach((obj) => {
                let input = obj.group.querySelector("input, textarea, select")

                if (isNaN(input.value) && input.value != "") {
                    this.invalidate(obj, "Campo deve conter apenas numeros.")
                }
            })
        }
    }

    hasInvalidSize() {
        this.sizedInputObjects.forEach(obj => {
            let input = obj.group.querySelector("input, textarea, select")
            let minSize = Number.parseInt(input.getAttribute("minTextSize"))
            if (input.value.length < minSize) {
                this.invalidate(obj, `O campo deve conter no mínimo ${minSize} caracteres.`)
            }
        })
    }

    hasInvalidWordCount() {
        this.inputObjectsWithWordCount.forEach(obj => {
            let input = obj.group.querySelector("input, textarea, select")
            let minSize = Number.parseInt(input.getAttribute("minWordCount"))
            if (input.value.split(" ").length < minSize && input.value != "") {
                this.invalidate(obj, `O campo deve conter no mínimo ${minSize} palavras.`)
            }
        })
    }

    addRealTimeContentValidation() {
        this.allInputObjects.forEach(obj => {
            let input = obj.group.querySelector("input, textarea, select")

            input.addEventListener("input", (event) => {
                if (input.value == "") {
                    this.invalidate(obj, "Campo deve ser preenchido.")
                } else if (isNaN(input.value) && obj.group.classList.contains("numeric")) {
                    this.invalidate(obj, "Campo deve conter apenas numeros.")
                }
                else {
                    this.validate(obj)
                }
            })
        })
    }

    isValid() {
        return this.allInputObjects.filter(element => !element.isValid) == 0 &&
            this.numericInputObjects.filter(element => !element.isValid) == 0 &&
            this.sizedInputObjects.filter(element => !element.isValid) == 0 &&
            this.inputObjectsWithWordCount.filter(element => !element.isValid) == 0
    }

    getValidableInputs() {
        let groups = Array.from(this.formRoot.querySelectorAll("div")).filter(element => {
            return element.querySelector(".invalid-feedback") != null
        }).filter(group => !group.classList.contains("no-validate"))
        return (groups == null) ? [] : groups.map(element => { return { group: element, isValid: true } })
    }

    invalidate(obj, message = "") {
        obj.group.querySelector("input, textarea, select").classList.add("is-invalid")
        obj.group.querySelector(".invalid-feedback").innerText = message
        obj.isValid = false
    }

    validate(obj) {
        obj.group.querySelector("input, textarea, select").classList.remove("is-invalid")
        obj.isValid = true
    }

    validateEmail(input) {
        let emailRegex = /\S+@\S+\.\S+/
        let divFeedback = document.querySelector(".email-feedback")
        if (emailRegex.test(input.value)) {
            input.classList.remove("is-invalid")
            return true
        } else {
            input.classList.add("is-invalid")
            divFeedback.innerText = "Insira um Email válido."
            return false
        }
    }
}