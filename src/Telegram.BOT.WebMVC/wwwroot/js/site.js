const validableForms = Array.from(document.getElementsByClassName("validate"))
const deleteConfirmationModal = document.getElementById("modalDeleteConfirmation")
const deleteButtons = Array.from(document.getElementsByClassName("delete-button"))
const confirmationButton = document.getElementById("deleteConfirmation")
if (validableForms != null) {
    validableForms.forEach(form => {
        let formValidator = new FormValidator(form);
        let isInvalid = false
        form.addEventListener("submit", event => {
            if (formValidator.allInputGroups.length > 0) {
                isInvalid = formValidator.hasEmpty()
            }
            if (formValidator.numericInputGroups.length > 0 && !isInvalid) {
                isInvalid = formValidator.hasInvalidNumbers()
            }
            if (formValidator.sizedInputGroups.length > 0 && !isInvalid) {
                isInvalid = formValidator.hasInvalidSize()
            }
            if (isInvalid) {
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
