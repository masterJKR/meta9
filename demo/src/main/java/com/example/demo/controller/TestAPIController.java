package com.example.demo.controller;

import com.example.demo.entity.ArrowData;
import com.example.demo.entity.TestJson;
import com.example.demo.service.TestService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api")
public class TestAPIController {
    @Autowired
    private TestService testService;

    //유니티에서 화살수량데이터를 받기위한 메서드
    @PostMapping("/datatest")
    public Map<String, Object> testDatatest(@RequestBody Map<String, Object> map) {
        System.out.println("데이터 : " + map);
        testService.arrowSave( map );
        return map;
    }

    // DB에 저장된 화살 수량 보내기
    @GetMapping("/getarrow")
    public ArrowData getarrow() {
        return testService.getArrow();
    }


    @GetMapping("/allData")
    public List<TestJson> all(){
        return testService.findAll();
    }

    @GetMapping("/test")  //  주소 ->  http://localhost:8080/api/test
    public Map<String, Object> getTest(){
        Map<String, Object> map = new HashMap<>();
        map.put("sceneName","Dungeon1");
        map.put("doorNumber",2);
        map.put("keys",3);
        map.put("arrows",34);
        map.put("hp",3);
        return map;
    }
}
