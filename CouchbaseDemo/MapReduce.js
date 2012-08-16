//GetUsersByPurchaseCountRange
function (doc) {
    if (doc.Type == "User") {
        emit(doc.PurchaseCount, null);
    }
}

//GetUsersByRole
function (doc) {
    if (doc.Type == "User") {
        for (i = 0; i < doc.Roles.length; i++) {
            var role = doc.Roles[i];
            emit(role.RoleName, doc);
        }
    }
}