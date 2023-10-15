const validableForms = Array.from(document.getElementsByClassName("validate"))
const deleteConfirmationModal = document.getElementById("modalDeleteConfirmation")
const deleteButtons = Array.from(document.getElementsByClassName("delete-button"))
const confirmationButton = document.getElementById("deleteConfirmation")
if (validableForms != null) {
    validableForms.forEach(form => {
        let formValidator = new FormValidator(form);
        let isInvalid = true
        form.addEventListener("submit", event => {
            if (formValidator.allInputObjects.length > 0) {
                formValidator.hasEmpty()
            }
            if (formValidator.numericInputObjects.length > 0) {
                formValidator.hasInvalidNumbers()
            }
            if (formValidator.sizedInputObjects.length > 0) {
                formValidator.hasInvalidSize()
            }
            if (formValidator.inputObjectsWithWordCount.length > 0) {
                formValidator.hasInvalidWordCount();
            }
            if (!formValidator.isValid()) {
                event.preventDefault()
                formValidator.addRealTimeContentValidation()
            }
        })
    })
}

deleteButtons.forEach(button => {
    button.addEventListener("click", event => {
        confirmationButton.href = button.href
    })
})

function deleteItem(link){
    window.location.href = link
}
