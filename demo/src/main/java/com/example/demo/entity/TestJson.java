package com.example.demo.entity;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

@Entity
@Table(name = "test_json")
@Getter
@Setter
public class TestJson {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    @Column(name="scene_name" , nullable = false)
    private String sceneName;

    private Integer doorNum;

    private Integer arrangeId;

    private String tag;
}
