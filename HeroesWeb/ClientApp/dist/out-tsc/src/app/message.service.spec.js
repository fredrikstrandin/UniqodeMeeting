import { TestBed, inject } from '@angular/core/testing';
import { MessageService } from './message.service';
describe('MessageService', function () {
    beforeEach(function () {
        TestBed.configureTestingModule({
            providers: [MessageService]
        });
    });
    it('should be created', inject([MessageService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=message.service.spec.js.map