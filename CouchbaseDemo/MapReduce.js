//GetUsersByPurchaseCountRange
function (doc) {
    emit(doc.PurchaseCount, null);
}

//GetUsersByRole
function (doc) {
    for (i = 0; i < doc.Roles.length; i++) {
        var role = doc.Roles[i];
        emit(role.RoleName, doc);
    }
}