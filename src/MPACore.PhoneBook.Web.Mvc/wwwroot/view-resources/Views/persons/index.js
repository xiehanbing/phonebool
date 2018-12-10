(function () {
    $(function () {
        $('.updateSpan').css('display', 'none');
        var _personService = abp.services.app.person;

        //添加联系人
        var _$modal = $('#PersonCreateModal');
        var _$form = _$modal.find('form');
        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();
            if (!_$form.valid()) {
                return;
            }
            var personEditDto = _$form.serializeFormToObject();//序列化表单为对象
            var phonenumberType = personEditDto.PhoneNumberType;
            var phoneNumber = personEditDto.PhoneNumber;
            personEditDto.PhoneNumbers = [{ Type: phonenumberType, Number: phoneNumber, Id: personEditDto.PhoneId }];
            abp.ui.setBusy(_$modal);
            //约定大于配置
            _personService.addOrUpdatePerson({ Person:personEditDto }).done(function () {
                _$modal.modal('hide');
                location.reload();
            }).always(function () {
                abp.ui.clearBusy(_$modal);

            });
        });
        //更新联系人
        $('.edit-person').click(function (e) {
            e.preventDefault();
            $('.updateSpan').css('display', '');
            $('.createSpan').css('display', 'none');
            var personId = $(this).attr('data-person-id');
            _personService.getPersonForEdit({ id: personId }).done(function(data) {
                $('input[name=Id]').val(data.person.id).parent().addClass('focused');
                $('input[name=Name]').val(data.person.name).parent().addClass('focused');
                $('input[name=Address]').val(data.person.address).parent().addClass('focused');
                $('input[name=Email]').val(data.person.email).parent().addClass('focused');
                var phoneNumbers = data.person.phoneNumbers;
                if (phoneNumbers != null && phoneNumbers.length > 0) {
                    $('select[name=PhoneNumberType]').selectpicker("val",phoneNumbers[0].type+'').parent().addClass('focused');
                    $('input[name=PhoneNumber]').val(phoneNumbers[0].number).parent().addClass('focused');
                    $('input[name=PhoneId]').val(phoneNumbers[0].id);
                }
            });
        });

        $('#PersonCreateModal').on('hide.bs.modal',
            function () {
                $('.updateSpan').css('display', 'none');
                $('.createSpan').css('display', '');
                $('input[name=Id]').parent().removeClass('focused');
                $('input[name=Name]').parent().removeClass('focused');
                $('input[name=Address]').parent().removeClass('focused');
                $('input[name=Email]').parent().removeClass('focused');
                _$form[0].reset();
            });
        //刷新
        $('#RefreshButton').click(function() {
            refreshPersonList();
        });

        function refreshPersonList() {
            location.reload();
        }

        $('.delete-person').click(function() {
            var personId = $(this).attr('data-person-id');
            var personName = $(this).attr('data-person-name');
            deletePerson(personId,personName);
        });
        function deletePerson(id, name) {
            abp.message.confirm("是否删除姓名为" + name + "的联系人", function(isConfirm) {

                if (isConfirm) {
                    _personService.deletePerson(id).done(function() {
                        refreshPersonList();
                    });
                }
            });
        }
    });
})();